-- ========================================================
-- SISTEMA ICE BAR - BASE DE DATOS SQL SERVER
-- Script de Creación de Base de Datos y Tablas
-- ========================================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'IceBarDB')
BEGIN
    CREATE DATABASE IceBarDB;
END
GO

USE IceBarDB;
GO

-- 1. TABLA: Usuarios
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Usuarios')
BEGIN
    CREATE TABLE Usuarios (
        IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
        NombreCompleto NVARCHAR(100) NOT NULL,
        NombreUsuario NVARCHAR(50) NOT NULL UNIQUE,
        Password NVARCHAR(100) NOT NULL,
        Rol NVARCHAR(20) NOT NULL, -- 'Administrador' o 'Trabajador'
        FechaRegistro DATETIME DEFAULT GETDATE(),
        Activo BIT DEFAULT 1
    );
END
GO

-- 2. TABLA: Productos
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Productos')
BEGIN
    CREATE TABLE Productos (
        IdProducto INT IDENTITY(1,1) PRIMARY KEY,
        Nombre NVARCHAR(150) NOT NULL, -- Ej: "Coca Cola 600ml", "Cerveza Corona 355ml"
        Precio DECIMAL(18,2) NOT NULL,
        Stock INT NOT NULL DEFAULT 0,
        Categoria NVARCHAR(50) DEFAULT 'General',
        FechaRegistro DATETIME DEFAULT GETDATE(),
        Activo BIT DEFAULT 1
    );
END
GO

-- 3. TABLA: Ventas
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Ventas')
BEGIN
    CREATE TABLE Ventas (
        IdVenta INT IDENTITY(1,1) PRIMARY KEY,
        NumeroTicket NVARCHAR(30) NOT NULL,
        IdUsuario INT NOT NULL,
        FechaHora DATETIME DEFAULT GETDATE(),
        Total DECIMAL(18,2) NOT NULL,
        MontoPagado DECIMAL(18,2) NOT NULL, -- Precio cancelado / pagado por el cliente
        Cambio DECIMAL(18,2) NOT NULL,
        TipoPago NVARCHAR(30) NOT NULL, -- 'Efectivo', 'Tarjeta', 'Transferencia'
        Notas NVARCHAR(250) NULL,
        CONSTRAINT FK_Ventas_Usuarios FOREIGN KEY (IdUsuario) REFERENCES Usuarios(IdUsuario)
    );
END
GO

-- 4. TABLA: DetalleVentas
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
END
GO

-- 5. TABLA: Incidencias
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
END
GO

-- ========================================================
-- DATOS INICIALES / SEEDING
-- ========================================================

-- Solo existe un administrador creado antes de todo
IF NOT EXISTS (SELECT * FROM Usuarios WHERE NombreUsuario = 'admin')
BEGIN
    INSERT INTO Usuarios (NombreCompleto, NombreUsuario, Password, Rol, Activo)
    VALUES ('Administrador del Sistema', 'admin', 'admin123', 'Administrador', 1);
END
GO

-- Trabajador de ejemplo inicial
IF NOT EXISTS (SELECT * FROM Usuarios WHERE NombreUsuario = 'trabajador1')
BEGIN
    INSERT INTO Usuarios (NombreCompleto, NombreUsuario, Password, Rol, Activo)
    VALUES ('Carlos Bartender', 'trabajador1', '1234', 'Trabajador', 1);
END
GO

-- Productos iniciales de prueba para el bar
IF NOT EXISTS (SELECT * FROM Productos WHERE Nombre = 'Coca Cola 600ml')
BEGIN
    INSERT INTO Productos (Nombre, Precio, Stock, Categoria)
    VALUES 
    ('Coca Cola 600ml', 35.00, 50, 'Refrescos'),
    ('Agua Mineral Peñafiel 600ml', 30.00, 40, 'Bebidas'),
    ('Cerveza Corona Extra 355ml', 45.00, 60, 'Cervezas'),
    ('Cerveza Victoria 355ml', 45.00, 55, 'Cervezas'),
    ('Cerveza Modelo Especial 355ml', 50.00, 48, 'Cervezas'),
    ('Tequila Don Julio 70 (Trago)', 120.00, 30, 'Licores'),
    ('Ron Bacardi Blanco (Trago)', 80.00, 35, 'Licores'),
    ('Vodka Absolut Azul (Trago)', 85.00, 25, 'Licores'),
    ('Whisky Red Label (Trago)', 95.00, 20, 'Licores'),
    ('Gin Tanqueray Tonic', 110.00, 20, 'Cocteleria'),
    ('Mojito Clasico', 90.00, 25, 'Cocteleria'),
    ('Papas Fritas Sazonadas', 65.00, 30, 'Snacks'),
    ('Nachos con Queso y Jalapeños', 85.00, 25, 'Snacks');
END
GO
