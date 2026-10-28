using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.SqlClient;
using IceBarSystem.Models;

namespace IceBarSystem.Data
{
    public static class DatabaseHelper
    {
        private static readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "icebar_db_config.txt");

        // Cadena de conexión por defecto (soporta LocalDB, SQL Server local o SQLExpress)
        private static string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=IceBarDB;Integrated Security=True;TrustServerCertificate=True;";

        public static string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                SaveConnectionString(value);
            }
        }

        static DatabaseHelper()
        {
            LoadConnectionString();
        }

        private static void LoadConnectionString()
        {
            try
            {
                if (File.Exists(ConfigPath))
                {
                    string saved = File.ReadAllText(ConfigPath).Trim();
                    if (!string.IsNullOrWhiteSpace(saved))
                    {
                        _connectionString = saved;
                    }
                }
            }
            catch
            {
                // Usa la cadena por defecto si no puede leer el archivo
            }
        }

        public static void SaveConnectionString(string connStr)
        {
            try
            {
                File.WriteAllText(ConfigPath, connStr);
            }
            catch
            {
                // Ignorar error de escritura en disco
            }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public static bool TestConnection(string connectionString, out string errorMessage)
        {
            try
            {
                using var conn = new SqlConnection(connectionString);
                conn.Open();
                errorMessage = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Inicializa la base de datos IceBarDB si no existe y crea las tablas necesarias y usuario Admin.
        /// </summary>
        public static void EnsureDatabaseSetup(string? connectionString = null) => EnsureDatabase(connectionString);

        public static void EnsureDatabase(string? connectionString = null)
        {
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                _connectionString = connectionString;
                SaveConnectionString(connectionString);
            }

            try
            {
                var builder = new SqlConnectionStringBuilder(_connectionString);
                string originalDatabase = builder.InitialCatalog;

                // Conectar primero al master para verificar y crear la base de datos si falta
                builder.InitialCatalog = "master";
                using (var masterConn = new SqlConnection(builder.ConnectionString))
                {
                    masterConn.Open();
                    string checkDbQuery = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{originalDatabase}') CREATE DATABASE [{originalDatabase}];";
                    using var cmdMaster = new SqlCommand(checkDbQuery, masterConn);
                    cmdMaster.ExecuteNonQuery();
                }

                // Ahora conectar a IceBarDB y crear las tablas
                using (var conn = GetConnection())
                {
                    conn.Open();

                    string createTablesScript = @"
                    -- TABLA USUARIOS
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuarios')
                    BEGIN
                        CREATE TABLE Usuarios (
                            IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
                            NombreCompleto NVARCHAR(100) NOT NULL,
                            NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
                            Password NVARCHAR(100) NOT NULL,
                            Rol NVARCHAR(20) NOT NULL,
                            FechaRegistro DATETIME DEFAULT GETDATE(),
                            Activo BIT DEFAULT 1
                        );
                    END;

                    -- TABLA PRODUCTOS
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Productos')
                    BEGIN
                        CREATE TABLE Productos (
                            IdProducto INT IDENTITY(1,1) PRIMARY KEY,
                            Nombre NVARCHAR(150) NOT NULL,
                            Precio DECIMAL(18,2) NOT NULL,
                            Stock INT NOT NULL DEFAULT 0,
                            Categoria NVARCHAR(50) DEFAULT 'General',
                            FechaRegistro DATETIME DEFAULT GETDATE(),
                            Activo BIT DEFAULT 1
                        );
                    END;

                    -- TABLA VENTAS
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Ventas')
                    BEGIN
                        CREATE TABLE Ventas (
                            IdVenta INT IDENTITY(1,1) PRIMARY KEY,
                            NumeroTicket NVARCHAR(30) NOT NULL,
                            IdUsuario INT NOT NULL,
                            FechaHora DATETIME DEFAULT GETDATE(),
                            Total DECIMAL(18,2) NOT NULL,
                            MontoPagado DECIMAL(18,2) NOT NULL,
                            Cambio DECIMAL(18,2) NOT NULL,
                            TipoPago NVARCHAR(30) NOT NULL,
                            Notas NVARCHAR(250) NULL,
                            CONSTRAINT FK_Ventas_Usuarios FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
                        );
                    END;

                    -- TABLA DETALLE VENTAS
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DetalleVentas')
                    BEGIN
                        CREATE TABLE DetalleVentas (
                            IdDetalle INT IDENTITY(1,1) PRIMARY KEY,
                            IdVenta INT NOT NULL,
                            IdProducto INT NOT NULL,
                            NombreProducto NVARCHAR(150) NOT NULL,
                            PrecioUnitario DECIMAL(18,2) NOT NULL,
                            Cantidad INT NOT NULL,
                            Subtotal DECIMAL(18,2) NOT NULL,
                            CONSTRAINT FK_DetalleVentas_Ventas FOREIGN KEY (IdVenta) REFERENCES Ventas(IdVenta) ON DELETE CASCADE,
                            CONSTRAINT FK_DetalleVentas_Productos FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto)
                        );
                    END;

                    -- TABLA INCIDENCIAS
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Incidencias')
                    BEGIN
                        CREATE TABLE Incidencias (
                            IdIncidencia INT IDENTITY(1,1) PRIMARY KEY,
                            IdUsuario INT NOT NULL,
                            Titulo NVARCHAR(100) NOT NULL,
                            Descripcion NVARCHAR(MAX) NOT NULL,
                            FechaHora DATETIME DEFAULT GETDATE(),
                            Estado NVARCHAR(30) DEFAULT 'Reportado',
                            CONSTRAINT FK_Incidencias_Usuarios FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
                        );
                    END;

                    -- SEEDING: Crear el Administrador único inicial si no existe
                    IF NOT EXISTS (SELECT * FROM Usuarios WHERE NombreUsuario = 'admin')
                    BEGIN
                        INSERT INTO Usuarios (NombreCompleto, NombreUsuario, Password, Rol, Activo)
                        VALUES ('Administrador Principal', 'admin', 'admin123', 'Administrador', 1);
                    END;

                    -- SEEDING: Crear trabajador demo si no existe
                    IF NOT EXISTS (SELECT * FROM Usuarios WHERE NombreUsuario = 'trabajador1')
                    BEGIN
                        INSERT INTO Usuarios (NombreCompleto, NombreUsuario, Password, Rol, Activo)
                        VALUES ('Carlos Bartender', 'trabajador1', '1234', 'Trabajador', 1);
                    END;

                    -- SEEDING: Productos de muestra para el bar
                    IF NOT EXISTS (SELECT * FROM Productos)
                    BEGIN
                        INSERT INTO Productos (Nombre, Precio, Stock, Categoria) VALUES
                        ('Coca Cola 600ml', 35.00, 45, 'Refrescos'),
                        ('Agua Mineral Peñafiel 600ml', 30.00, 35, 'Bebidas'),
                        ('Cerveza Corona Extra 355ml', 45.00, 60, 'Cervezas'),
                        ('Cerveza Victoria 355ml', 45.00, 50, 'Cervezas'),
                        ('Cerveza Modelo Especial 355ml', 50.00, 40, 'Cervezas'),
                        ('Tequila Don Julio 70 (Copa)', 130.00, 25, 'Licores'),
                        ('Ron Bacardi Blanco (Trago)', 80.00, 30, 'Licores'),
                        ('Vodka Absolut Azul (Trago)', 85.00, 25, 'Licores'),
                        ('Whisky Red Label (Trago)', 95.00, 20, 'Licores'),
                        ('Mojito Cubano Clásico', 90.00, 30, 'Cocteleria'),
                        ('Gin Tonic Helado', 110.00, 20, 'Cocteleria'),
                        ('Papas Sazonadas ICE BAR', 65.00, 25, 'Snacks'),
                        ('Nachos con Queso y Jalapeño', 85.00, 25, 'Snacks');
                    END;
                    ";

                    using var cmdTables = new SqlCommand(createTablesScript, conn);
                    cmdTables.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al inicializar la base de datos de ICE BAR: {ex.Message}", ex);
            }
        }

        // ==========================================
        // MÉTODOS DE USUARIOS Y AUTENTICACIÓN
        // ==========================================
        public static Usuario? IniciarSesion(string nombreUsuario, string password)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = "SELECT IdUsuario, NombreCompleto, NombreUsuario, Password, Rol, FechaRegistro, Activo " +
                           "FROM Usuarios WHERE NombreUsuario = @Usuario AND Password = @Password AND Activo = 1";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Usuario", nombreUsuario);
            cmd.Parameters.AddWithValue("@Password", password);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    IdUsuario = reader.GetInt32(0),
                    NombreCompleto = reader.GetString(1),
                    NombreUsuario = reader.GetString(2),
                    Password = reader.GetString(3),
                    Rol = reader.GetString(4),
                    FechaRegistro = reader.GetDateTime(5),
                    Activo = reader.GetBoolean(6)
                };
            }

            return null;
        }

        public static List<Usuario> ObtenerUsuarios()
        {
            var lista = new List<Usuario>();
            using var conn = GetConnection();
            conn.Open();

            string query = "SELECT IdUsuario, NombreCompleto, NombreUsuario, Password, Rol, FechaRegistro, Activo FROM Usuarios ORDER BY Rol DESC, NombreCompleto ASC";
            using var cmd = new SqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Usuario
                {
                    IdUsuario = reader.GetInt32(0),
                    NombreCompleto = reader.GetString(1),
                    NombreUsuario = reader.GetString(2),
                    Password = reader.GetString(3),
                    Rol = reader.GetString(4),
                    FechaRegistro = reader.GetDateTime(5),
                    Activo = reader.GetBoolean(6)
                });
            }
            return lista;
        }

        public static void RegistrarTrabajador(string nombreCompleto, string nombreUsuario, string password)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = "INSERT INTO Usuarios (NombreCompleto, NombreUsuario, Password, Rol, Activo) " +
                           "VALUES (@Nombre, @Usuario, @Pass, 'Trabajador', 1)";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nombre", nombreCompleto);
            cmd.Parameters.AddWithValue("@Usuario", nombreUsuario);
            cmd.Parameters.AddWithValue("@Pass", password);
            cmd.ExecuteNonQuery();
        }

        public static void CambiarEstadoUsuario(int idUsuario, bool activo)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = "UPDATE Usuarios SET Activo = @Activo WHERE IdUsuario = @Id AND Rol <> 'Administrador'";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Activo", activo);
            cmd.Parameters.AddWithValue("@Id", idUsuario);
            cmd.ExecuteNonQuery();
        }

        public static void EliminarTrabajador(int idUsuario)
        {
            using var conn = GetConnection();
            conn.Open();

            // Protege al administrador de ser eliminado
            string query = "DELETE FROM Usuarios WHERE IdUsuario = @Id AND Rol <> 'Administrador'";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", idUsuario);
            cmd.ExecuteNonQuery();
        }

        // ==========================================
        // MÉTODOS DE PRODUCTOS Y STOCK
        // ==========================================
        public static List<Producto> ObtenerProductos(string filtro = "")
        {
            var lista = new List<Producto>();
            using var conn = GetConnection();
            conn.Open();

            string query = "SELECT IdProducto, Nombre, Precio, Stock, Categoria, FechaRegistro, Activo " +
                           "FROM Productos WHERE Activo = 1 ";

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                query += "AND (Nombre LIKE @Filtro OR Categoria LIKE @Filtro) ";
            }
            query += "ORDER BY Nombre ASC";

            using var cmd = new SqlCommand(query, conn);
            if (!string.IsNullOrWhiteSpace(filtro))
            {
                cmd.Parameters.AddWithValue("@Filtro", "%" + filtro + "%");
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Producto
                {
                    IdProducto = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Precio = reader.GetDecimal(2),
                    Stock = reader.GetInt32(3),
                    Categoria = reader.IsDBNull(4) ? "General" : reader.GetString(4),
                    FechaRegistro = reader.GetDateTime(5),
                    Activo = reader.GetBoolean(6)
                });
            }

            return lista;
        }

        public static void AgregarProducto(string nombre, decimal precio, int stockInicial, string categoria = "General")
        {
            using var conn = GetConnection();
            conn.Open();

            string query = "INSERT INTO Productos (Nombre, Precio, Stock, Categoria, Activo) " +
                           "VALUES (@Nombre, @Precio, @Stock, @Categoria, 1)";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@Precio", precio);
            cmd.Parameters.AddWithValue("@Stock", stockInicial);
            cmd.Parameters.AddWithValue("@Categoria", categoria);
            cmd.ExecuteNonQuery();
        }

        public static void EditarProducto(int idProducto, string nombre, decimal precio, string categoria = "General")
        {
            using var conn = GetConnection();
            conn.Open();

            string query = "UPDATE Productos SET Nombre = @Nombre, Precio = @Precio, Categoria = @Categoria WHERE IdProducto = @Id";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nombre", nombre);
            cmd.Parameters.AddWithValue("@Precio", precio);
            cmd.Parameters.AddWithValue("@Categoria", categoria);
            cmd.Parameters.AddWithValue("@Id", idProducto);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Actualiza el stock de forma acumulativa sumando la cantidad ingresada a la cantidad anterior
        /// </summary>
        public static void AumentarStock(int idProducto, int cantidadASumar)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = "UPDATE Productos SET Stock = Stock + @Cantidad WHERE IdProducto = @Id";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Cantidad", cantidadASumar);
            cmd.Parameters.AddWithValue("@Id", idProducto);
            cmd.ExecuteNonQuery();
        }

        public static void EliminarProducto(int idProducto)
        {
            using var conn = GetConnection();
            conn.Open();

            // Marcado suave o borrado
            string query = "UPDATE Productos SET Activo = 0 WHERE IdProducto = @Id";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Id", idProducto);
            cmd.ExecuteNonQuery();
        }

        // ==========================================
        // MÉTODOS DE VENTAS
        // ==========================================
        public static (bool Exito, string Mensaje, int IdVenta) RegistrarVenta(Venta venta)
        {
            using var conn = GetConnection();
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                // 1. Validar stock de cada producto primero
                foreach (var item in venta.Detalles)
                {
                    string checkStock = "SELECT Stock FROM Productos WHERE IdProducto = @Id AND Activo = 1";
                    using var cmdCheck = new SqlCommand(checkStock, conn, tx);
                    cmdCheck.Parameters.AddWithValue("@Id", item.IdProducto);
                    object? result = cmdCheck.ExecuteScalar();

                    if (result == null)
                    {
                        tx.Rollback();
                        return (false, $"El producto '{item.NombreProducto}' no existe o está inactivo.", 0);
                    }

                    int stockActual = Convert.ToInt32(result);
                    if (stockActual < item.Cantidad)
                    {
                        tx.Rollback();
                        return (false, $"Stock insuficiente para '{item.NombreProducto}'. Disponible: {stockActual}, solicitado: {item.Cantidad}.", 0);
                    }
                }

                // 2. Insertar cabecera de Venta
                string insertVenta = @"
                    INSERT INTO Ventas (NumeroTicket, IdUsuario, FechaHora, Total, MontoPagado, Cambio, TipoPago, Notas)
                    OUTPUT INSERTED.IdVenta
                    VALUES (@Ticket, @IdUsuario, @FechaHora, @Total, @MontoPagado, @Cambio, @TipoPago, @Notas);";

                int idVentaGenerada;
                using (var cmdVenta = new SqlCommand(insertVenta, conn, tx))
                {
                    cmdVenta.Parameters.AddWithValue("@Ticket", venta.NumeroTicket);
                    cmdVenta.Parameters.AddWithValue("@IdUsuario", venta.IdUsuario);
                    cmdVenta.Parameters.AddWithValue("@FechaHora", venta.FechaHora);
                    cmdVenta.Parameters.AddWithValue("@Total", venta.Total);
                    cmdVenta.Parameters.AddWithValue("@MontoPagado", venta.MontoPagado);
                    cmdVenta.Parameters.AddWithValue("@Cambio", venta.Cambio);
                    cmdVenta.Parameters.AddWithValue("@TipoPago", venta.TipoPago);
                    cmdVenta.Parameters.AddWithValue("@Notas", (object?)venta.Notas ?? DBNull.Value);

                    idVentaGenerada = (int)cmdVenta.ExecuteScalar()!;
                }

                // 3. Insertar Detalles de Venta y Descontar Stock
                string insertDetalle = @"
                    INSERT INTO DetalleVentas (IdVenta, IdProducto, NombreProducto, PrecioUnitario, Cantidad, Subtotal)
                    VALUES (@IdVenta, @IdProducto, @NombreProducto, @PrecioUnitario, @Cantidad, @Subtotal);";

                string descontarStock = "UPDATE Productos SET Stock = Stock - @Cantidad WHERE IdProducto = @IdProducto;";

                foreach (var item in venta.Detalles)
                {
                    using (var cmdDetalle = new SqlCommand(insertDetalle, conn, tx))
                    {
                        cmdDetalle.Parameters.AddWithValue("@IdVenta", idVentaGenerada);
                        cmdDetalle.Parameters.AddWithValue("@IdProducto", item.IdProducto);
                        cmdDetalle.Parameters.AddWithValue("@NombreProducto", item.NombreProducto);
                        cmdDetalle.Parameters.AddWithValue("@PrecioUnitario", item.PrecioUnitario);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@Subtotal", item.Subtotal);
                        cmdDetalle.ExecuteNonQuery();
                    }

                    using (var cmdStock = new SqlCommand(descontarStock, conn, tx))
                    {
                        cmdStock.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                        cmdStock.Parameters.AddWithValue("@IdProducto", item.IdProducto);
                        cmdStock.ExecuteNonQuery();
                    }
                }

                tx.Commit();
                return (true, "Venta registrada con éxito.", idVentaGenerada);
            }
            catch (Exception ex)
            {
                tx.Rollback();
                return (false, $"Error al registrar la venta: {ex.Message}", 0);
            }
        }

        public static List<Venta> ObtenerHistorialVentas(DateTime? desde = null, DateTime? hasta = null)
        {
            var lista = new List<Venta>();
            using var conn = GetConnection();
            conn.Open();

            string query = @"
                SELECT v.IdVenta, v.NumeroTicket, v.IdUsuario, u.NombreCompleto, v.FechaHora, 
                       v.Total, v.MontoPagado, v.Cambio, v.TipoPago, v.Notas
                FROM Ventas v
                INNER JOIN Usuarios u ON v.IdUsuario = u.IdUsuario
                WHERE 1=1 ";

            if (desde.HasValue)
            {
                query += "AND v.FechaHora >= @Desde ";
            }
            if (hasta.HasValue)
            {
                query += "AND v.FechaHora <= @Hasta ";
            }

            query += "ORDER BY v.FechaHora DESC";

            using var cmd = new SqlCommand(query, conn);
            if (desde.HasValue) cmd.Parameters.AddWithValue("@Desde", desde.Value.Date);
            if (hasta.HasValue) cmd.Parameters.AddWithValue("@Hasta", hasta.Value.Date.AddDays(1).AddTicks(-1));

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Venta
                {
                    IdVenta = reader.GetInt32(0),
                    NumeroTicket = reader.GetString(1),
                    IdUsuario = reader.GetInt32(2),
                    NombreUsuario = reader.GetString(3),
                    FechaHora = reader.GetDateTime(4),
                    Total = reader.GetDecimal(5),
                    MontoPagado = reader.GetDecimal(6),
                    Cambio = reader.GetDecimal(7),
                    TipoPago = reader.GetString(8),
                    Notas = reader.IsDBNull(9) ? null : reader.GetString(9)
                });
            }

            return lista;
        }

        public static List<DetalleVenta> ObtenerDetallesDeVenta(int idVenta)
        {
            var lista = new List<DetalleVenta>();
            using var conn = GetConnection();
            conn.Open();

            string query = "SELECT IdDetalle, IdVenta, IdProducto, NombreProducto, PrecioUnitario, Cantidad, Subtotal " +
                           "FROM DetalleVentas WHERE IdVenta = @IdVenta";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@IdVenta", idVenta);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new DetalleVenta
                {
                    IdDetalle = reader.GetInt32(0),
                    IdVenta = reader.GetInt32(1),
                    IdProducto = reader.GetInt32(2),
                    NombreProducto = reader.GetString(3),
                    PrecioUnitario = reader.GetDecimal(4),
                    Cantidad = reader.GetInt32(5),
                    Subtotal = reader.GetDecimal(6)
                });
            }

            return lista;
        }

        // ==========================================
        // MÉTODOS DE INCIDENCIAS
        // ==========================================
        public static void RegistrarIncidencia(int idUsuario, string titulo, string descripcion)
        {
            using var conn = GetConnection();
            conn.Open();

            string query = "INSERT INTO Incidencias (IdUsuario, Titulo, Descripcion, FechaHora, Estado) " +
                           "VALUES (@IdUsuario, @Titulo, @Descripcion, GETDATE(), 'Reportado')";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@IdUsuario", idUsuario);
            cmd.Parameters.AddWithValue("@Titulo", titulo);
            cmd.Parameters.AddWithValue("@Descripcion", descripcion);
            cmd.ExecuteNonQuery();
        }

        public static List<Incidencia> ObtenerIncidencias()
        {
            var lista = new List<Incidencia>();
            using var conn = GetConnection();
            conn.Open();

            string query = @"
                SELECT i.IdIncidencia, i.IdUsuario, u.NombreCompleto, i.Titulo, i.Descripcion, i.FechaHora, i.Estado
                FROM Incidencias i
                INNER JOIN Usuarios u ON i.IdUsuario = u.IdUsuario
                ORDER BY i.FechaHora DESC";

            using var cmd = new SqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Incidencia
                {
                    IdIncidencia = reader.GetInt32(0),
                    IdUsuario = reader.GetInt32(1),
                    NombreUsuario = reader.GetString(2),
                    Titulo = reader.GetString(3),
                    Descripcion = reader.GetString(4),
                    FechaHora = reader.GetDateTime(5),
                    Estado = reader.GetString(6)
                });
            }

            return lista;
        }
    }
}
