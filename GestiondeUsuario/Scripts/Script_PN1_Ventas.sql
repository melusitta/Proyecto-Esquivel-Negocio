-- =====================================================================
-- PN1 - Registro y venta de pedido
-- Tablas de negocio, productos de ejemplo y permisos (Vendedor / Cajero)
-- Se puede ejecutar más de una vez: no duplica tablas ni datos.
-- Después de ejecutarlo, al entrar con el Admin saltará la inconsistencia
-- del dígito verificador: tocar "Recalcular DV".
-- =====================================================================
USE GestionUsuarios
GO

-- ---------------------------------------------------------------------
-- Producto
-- ---------------------------------------------------------------------
IF OBJECT_ID('Producto', 'U') IS NULL
CREATE TABLE Producto (
    Id              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Producto PRIMARY KEY,
    CodigoProducto  INT             NOT NULL CONSTRAINT UQ_Producto_Codigo UNIQUE,
    Nombre          NVARCHAR(100)   NOT NULL,
    Marca           NVARCHAR(50)    NOT NULL,
    Color           NVARCHAR(30)    NOT NULL,
    Modelo          NVARCHAR(50)    NOT NULL,
    PrecioUnitario  DECIMAL(12,2)   NOT NULL CONSTRAINT CK_Producto_Precio CHECK (PrecioUnitario >= 0),
    Existencia      INT             NOT NULL CONSTRAINT CK_Producto_Existencia CHECK (Existencia >= 0),
    Activo          BIT             NOT NULL CONSTRAINT DF_Producto_Activo DEFAULT 1
)
GO

-- ---------------------------------------------------------------------
-- Cliente (lo registra el Cajero)
-- ---------------------------------------------------------------------
IF OBJECT_ID('Cliente', 'U') IS NULL
CREATE TABLE Cliente (
    Id              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Cliente PRIMARY KEY,
    DNI             INT             NOT NULL CONSTRAINT UQ_Cliente_DNI UNIQUE,
    Nombre          NVARCHAR(100)   NOT NULL,
    Apellido        NVARCHAR(100)   NOT NULL,
    Telefono        NVARCHAR(30)    NOT NULL,
    Email           NVARCHAR(100)   NOT NULL,
    Direccion       NVARCHAR(150)   NOT NULL,
    Localidad       NVARCHAR(100)   NOT NULL,
    CodigoPostal    NVARCHAR(10)    NOT NULL,
    FechaAlta       DATETIME        NOT NULL CONSTRAINT DF_Cliente_FechaAlta DEFAULT GETDATE()
)
GO

-- ---------------------------------------------------------------------
-- Carrito (lo carga el Vendedor y lo asocia al DNI del cliente).
-- El DNI no es FK: el cliente puede registrarse recién en la caja.
-- Estado: Abierto -> Asociado -> Facturado
-- ---------------------------------------------------------------------
IF OBJECT_ID('Carrito', 'U') IS NULL
CREATE TABLE Carrito (
    Id              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Carrito PRIMARY KEY,
    DNI             INT             NULL,
    Estado          NVARCHAR(20)    NOT NULL CONSTRAINT DF_Carrito_Estado DEFAULT 'Abierto',
    FechaCreacion   DATETIME        NOT NULL CONSTRAINT DF_Carrito_Fecha DEFAULT GETDATE(),
    Vendedor        NVARCHAR(100)   NOT NULL,
    CONSTRAINT CK_Carrito_Estado CHECK (Estado IN ('Abierto', 'Asociado', 'Facturado'))
)
GO

IF OBJECT_ID('ItemCarrito', 'U') IS NULL
CREATE TABLE ItemCarrito (
    IdCarrito       INT             NOT NULL CONSTRAINT FK_ItemCarrito_Carrito REFERENCES Carrito(Id),
    IdProducto      INT             NOT NULL CONSTRAINT FK_ItemCarrito_Producto REFERENCES Producto(Id),
    Cantidad        INT             NOT NULL CONSTRAINT CK_ItemCarrito_Cantidad CHECK (Cantidad > 0),
    PrecioUnitario  DECIMAL(12,2)   NOT NULL,
    CONSTRAINT PK_ItemCarrito PRIMARY KEY (IdCarrito, IdProducto)
)
GO

-- ---------------------------------------------------------------------
-- Factura (la genera el Cajero). Guarda DNI y nombre del cliente al
-- momento de la venta. Estado: Pendiente -> Pagada
-- ---------------------------------------------------------------------
IF OBJECT_ID('Factura', 'U') IS NULL
CREATE TABLE Factura (
    Id              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Factura PRIMARY KEY,
    NroFactura      INT             NOT NULL CONSTRAINT UQ_Factura_Nro UNIQUE,
    IdCarrito       INT             NOT NULL CONSTRAINT FK_Factura_Carrito REFERENCES Carrito(Id)
                                             CONSTRAINT UQ_Factura_Carrito UNIQUE,
    IdCliente       INT             NOT NULL CONSTRAINT FK_Factura_Cliente REFERENCES Cliente(Id),
    DNI             INT             NOT NULL,
    NombreCliente   NVARCHAR(201)   NOT NULL,
    FechaHora       DATETIME        NOT NULL,
    Total           DECIMAL(12,2)   NOT NULL,
    Estado          NVARCHAR(20)    NOT NULL CONSTRAINT DF_Factura_Estado DEFAULT 'Pendiente',
    FormaPago       NVARCHAR(30)    NULL,
    MontoPagado     DECIMAL(12,2)   NULL,
    FechaPago       DATETIME        NULL,
    Cajero          NVARCHAR(100)   NOT NULL,
    CONSTRAINT CK_Factura_Estado CHECK (Estado IN ('Pendiente', 'Pagada'))
)
GO

IF OBJECT_ID('ItemFactura', 'U') IS NULL
CREATE TABLE ItemFactura (
    IdFactura       INT             NOT NULL CONSTRAINT FK_ItemFactura_Factura REFERENCES Factura(Id),
    IdProducto      INT             NOT NULL CONSTRAINT FK_ItemFactura_Producto REFERENCES Producto(Id),
    Cantidad        INT             NOT NULL CONSTRAINT CK_ItemFactura_Cantidad CHECK (Cantidad > 0),
    PrecioUnitario  DECIMAL(12,2)   NOT NULL,
    CONSTRAINT PK_ItemFactura PRIMARY KEY (IdFactura, IdProducto)
)
GO

-- ---------------------------------------------------------------------
-- Productos de ejemplo
-- ---------------------------------------------------------------------
MERGE Producto AS destino
USING (VALUES
    (1001, N'Peluche Oso Abrazos',        N'Plumy',      N'Marrón',   N'Clásico 30 cm',     15990.00, 25),
    (1002, N'Peluche Unicornio Brillante', N'Plumy',     N'Lila',     N'Mágico 40 cm',      21500.00, 18),
    (1003, N'Peluche Pony Estrellita',    N'Plumy',      N'Rosa',     N'Pony 25 cm',        18750.00, 12),
    (1004, N'Muñeca Sofía',               N'Cariñitos',  N'Rosa',     N'Vestido de gala',   24990.00, 10),
    (1005, N'Bloques de Construcción',    N'ArmaTodo',   N'Multicolor', N'Set 120 piezas',  32500.00, 15),
    (1006, N'Auto a Control Remoto',      N'Turbo Kids', N'Rojo',     N'Rally 1:18',        45900.00,  8),
    (1007, N'Rompecabezas Animales',      N'Mentecitas', N'Multicolor', N'500 piezas',      12300.00, 20),
    (1008, N'Pelota Saltarina',           N'Rebote',     N'Celeste',  N'Mediana 20 cm',      6490.00, 40),
    (1009, N'Cocinita de Juguete',        N'MiniChef',   N'Blanco',   N'Con sonidos',       58900.00,  5),
    (1010, N'Peluche Dinosaurio Rex',     N'Plumy',      N'Verde',    N'Jurásico 35 cm',    19990.00, 14)
) AS origen (CodigoProducto, Nombre, Marca, Color, Modelo, PrecioUnitario, Existencia)
ON destino.CodigoProducto = origen.CodigoProducto
WHEN NOT MATCHED THEN
    INSERT (CodigoProducto, Nombre, Marca, Color, Modelo, PrecioUnitario, Existencia)
    VALUES (origen.CodigoProducto, origen.Nombre, origen.Marca, origen.Color, origen.Modelo, origen.PrecioUnitario, origen.Existencia);
GO

-- ---------------------------------------------------------------------
-- Permisos del PN1 (composite):
--   Patente CargarCarrito -> Vendedor (CUN-001, CUN-002)
--   Patente Facturar      -> Cajero   (CUN-003, CUN-004, CUN-005)
--   Familia Ventas        -> contiene las dos; se asigna al Admin
-- ---------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM Patente WHERE Nombre = 'CargarCarrito')
    INSERT INTO Patente (Nombre, Descripcion) VALUES ('CargarCarrito', N'Permite cargar carritos y asociarlos al cliente');
IF NOT EXISTS (SELECT 1 FROM Patente WHERE Nombre = 'Facturar')
    INSERT INTO Patente (Nombre, Descripcion) VALUES ('Facturar', N'Permite registrar clientes, generar facturas y cobrar ventas');
IF NOT EXISTS (SELECT 1 FROM Familia WHERE Nombre = 'Ventas')
    INSERT INTO Familia (Nombre, Descripcion) VALUES ('Ventas', N'Proceso completo de venta (PN1)');
IF NOT EXISTS (SELECT 1 FROM Rol WHERE Nombre = 'Vendedor')
    INSERT INTO Rol (Nombre, Descripcion) VALUES ('Vendedor', N'Atiende al cliente y carga el carrito');
IF NOT EXISTS (SELECT 1 FROM Rol WHERE Nombre = 'Cajero')
    INSERT INTO Rol (Nombre, Descripcion) VALUES ('Cajero', N'Registra clientes, factura y cobra');
GO

DECLARE @patCarrito INT = (SELECT Id FROM Patente WHERE Nombre = 'CargarCarrito');
DECLARE @patFacturar INT = (SELECT Id FROM Patente WHERE Nombre = 'Facturar');
DECLARE @famVentas INT = (SELECT Id FROM Familia WHERE Nombre = 'Ventas');
DECLARE @rolVendedor INT = (SELECT Id FROM Rol WHERE Nombre = 'Vendedor');
DECLARE @rolCajero INT = (SELECT Id FROM Rol WHERE Nombre = 'Cajero');
DECLARE @rolAdmin INT = (SELECT Id FROM Rol WHERE Nombre = 'Admin');

IF NOT EXISTS (SELECT 1 FROM Fam_Pat WHERE IdFamilia = @famVentas AND IdPatente = @patCarrito)
    INSERT INTO Fam_Pat (IdFamilia, IdPatente) VALUES (@famVentas, @patCarrito);
IF NOT EXISTS (SELECT 1 FROM Fam_Pat WHERE IdFamilia = @famVentas AND IdPatente = @patFacturar)
    INSERT INTO Fam_Pat (IdFamilia, IdPatente) VALUES (@famVentas, @patFacturar);
IF NOT EXISTS (SELECT 1 FROM Rol_Pat WHERE IdRol = @rolVendedor AND IdPatente = @patCarrito)
    INSERT INTO Rol_Pat (IdRol, IdPatente) VALUES (@rolVendedor, @patCarrito);
IF NOT EXISTS (SELECT 1 FROM Rol_Pat WHERE IdRol = @rolCajero AND IdPatente = @patFacturar)
    INSERT INTO Rol_Pat (IdRol, IdPatente) VALUES (@rolCajero, @patFacturar);
IF @rolAdmin IS NOT NULL AND NOT EXISTS (SELECT 1 FROM Rol_Fam WHERE IdRol = @rolAdmin AND IdFamilia = @famVentas)
    INSERT INTO Rol_Fam (IdRol, IdFamilia) VALUES (@rolAdmin, @famVentas);
GO
