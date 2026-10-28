using System;
using Microsoft.Data.SqlClient;
using IceBarSystem.Data;

namespace IceBarSystem.Helpers
{
    public static class AuditoriaHelper
    {
        public static void RegistrarAccion(int usuarioId, string modulo, string accion, string descripcion, string? detalles = null)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();

                    // Comprobar si la tabla Auditoria existe, si no, crearla
                    string checkTableQuery = @"
                        IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Auditoria')
                        BEGIN
                            CREATE TABLE Auditoria (
                                IdAuditoria INT IDENTITY(1,1) PRIMARY KEY,
                                IdUsuario INT NULL,
                                Modulo NVARCHAR(50) NOT NULL,
                                Accion NVARCHAR(50) NOT NULL,
                                Descripcion NVARCHAR(255) NOT NULL,
                                Detalles NVARCHAR(MAX) NULL,
                                FechaHora DATETIME DEFAULT GETDATE()
                            );
                        END";

                    using (SqlCommand cmdTable = new SqlCommand(checkTableQuery, conn))
                    {
                        cmdTable.ExecuteNonQuery();
                    }

                    string insertQuery = @"
                        INSERT INTO Auditoria (IdUsuario, Modulo, Accion, Descripcion, Detalles, FechaHora)
                        VALUES (@IdUsuario, @Modulo, @Accion, @Descripcion, @Detalles, GETDATE())";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@IdUsuario", (object?)usuarioId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@Modulo", modulo ?? "General");
                        cmd.Parameters.AddWithValue("@Accion", accion ?? "Info");
                        cmd.Parameters.AddWithValue("@Descripcion", descripcion ?? "");
                        cmd.Parameters.AddWithValue("@Detalles", (object?)detalles ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // No interrumpir el flujo si la auditoría falla
            }
        }
    }
}

