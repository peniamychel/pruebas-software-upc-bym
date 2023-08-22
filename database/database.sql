



/*

create database upd_database

use upd_database


USUARIOS
CATEGORIA_PRODUCTO
PRODUCTO
DETALLE_CARRITO
CARRITO_COMPRA

DROP TABLE USUARIOS;
DROP TABLE CATEGORIA_PRODUCTO;
DROP TABLE PRODUCTO;
DROP TABLE DETALLE_CARRITO;
DROP TABLE CARRITO_COMPRA;


*/


--/////////////////////////USUARIOS///////////////////////////////////////////

IF OBJECT_ID('USUARIOS', 'U') IS NOT NULL 
  DROP TABLE USUARIOS; 
GO

CREATE TABLE USUARIOS
(
  "ID"                          INT IDENTITY(1,1),
  "USER_NAME"                   VARCHAR(40) NOT NULL,
  "NOMBRE_COMPLETO"             VARCHAR(100) NOT NULL,
  "PASSWORD"		            VARCHAR(100) NOT NULL,
  "USUARIO_REGISTRO"            VARCHAR(50) DEFAULT SYSTEM_USER NOT NULL,
  "FECHA_REGISTRO"              DATETIME DEFAULT getdate() NOT NULL,
  "ESTADO_REGISTRO"				INT DEFAULT 1 NOT NULL, 
  CONSTRAINT USUARIOS_PK		PRIMARY KEY (ID)
);

--/////////////////////////CATEGORIA_PRODUCTO///////////////////////////////////////////

IF OBJECT_ID('CATEGORIA_PRODUCTO', 'U') IS NOT NULL 
  DROP TABLE CATEGORIA; 
GO

CREATE TABLE CATEGORIA_PRODUCTO
(
  "ID"								INT IDENTITY(1,1),
  "NOMBRE"							VARCHAR(100) NOT NULL,
  "USUARIO_REGISTRO"				VARCHAR(50) DEFAULT SYSTEM_USER NOT NULL,
  "FECHA_REGISTRO"					DATETIME DEFAULT getdate() NOT NULL,
  "ESTADO_REGISTRO"					INT DEFAULT 1 NOT NULL, 
  CONSTRAINT CATEGORIA_PRODUCTO_PK	PRIMARY KEY (ID)
);

--/////////////////////////PRODUCTO///////////////////////////////////////////

IF OBJECT_ID('PRODUCTO', 'U') IS NOT NULL 
  DROP TABLE PRODUCTO; 
GO

CREATE TABLE PRODUCTO
(
  "ID"                          INT IDENTITY(1,1),
  "NOMBRE"						VARCHAR(100) NOT NULL,
  "ID_CATEGORIA"				INT NOT NULL,
  "USUARIO_REGISTRO"            VARCHAR(50) DEFAULT SYSTEM_USER NOT NULL,
  "FECHA_REGISTRO"              DATETIME DEFAULT getdate() NOT NULL,
  "ESTADO_REGISTRO"				INT DEFAULT 1 NOT NULL, 
  CONSTRAINT PRODUCTO_PK		PRIMARY KEY (ID)
);

ALTER TABLE PRODUCTO
  ADD CONSTRAINT "FK_PRODUCTO_TO_CATEGORIA_PRODUCTO" 
  FOREIGN KEY(ID_CATEGORIA)
  REFERENCES CATEGORIA_PRODUCTO("ID");
  
  
--/////////////////////////CARRITO_COMPRA///////////////////////////////////////////

IF OBJECT_ID('CARRITO_COMPRA', 'U') IS NOT NULL 
  DROP TABLE CARRITO_COMPRA; 
GO

CREATE TABLE CARRITO_COMPRA
(
  "ID"                          INT IDENTITY(1,1),
  "FECHA"						DATETIME NOT NULL,
  "ID_USUARIO"					INT NOT NULL,
  "USUARIO_REGISTRO"            VARCHAR(50) DEFAULT SYSTEM_USER NOT NULL,
  "FECHA_REGISTRO"              DATETIME DEFAULT getdate() NOT NULL,
  "ESTADO_REGISTRO"				INT DEFAULT 1 NOT NULL, 
  CONSTRAINT CARRITO_COMPRA_PK		PRIMARY KEY (ID)
);

ALTER TABLE CARRITO_COMPRA
  ADD CONSTRAINT "FK_CARRITO_COMPRA_TO_USUARIO" 
  FOREIGN KEY("ID_USUARIO")
  REFERENCES USUARIOS("ID");


--/////////////////////////DETALLE_CARRITO///////////////////////////////////////////

IF OBJECT_ID('DETALLE_CARRITO', 'U') IS NOT NULL 
  DROP TABLE DETALLE_CARRITO; 
GO

CREATE TABLE DETALLE_CARRITO
(
  "ID"								INT IDENTITY(1,1),
  "CANTIDAD"						INT NOT NULL,
  "ID_PRODUCTO"						INT NOT NULL,
  "ID_CARRITO_COMPRA"				INT NOT NULL,
  "USUARIO_REGISTRO"				VARCHAR(50) DEFAULT SYSTEM_USER NOT NULL,
  "FECHA_REGISTRO"					DATETIME DEFAULT getdate() NOT NULL,
  "ESTADO_REGISTRO"					INT DEFAULT 1 NOT NULL, 
  CONSTRAINT DETALLE_CARRITO_PK		PRIMARY KEY (ID)
);

ALTER TABLE DETALLE_CARRITO
  ADD CONSTRAINT "FK_DETALLE_CARRITO_TO_PRODUCTO" 
  FOREIGN KEY("ID_PRODUCTO")
  REFERENCES PRODUCTO("ID");

ALTER TABLE DETALLE_CARRITO
  ADD CONSTRAINT "FK_DETALLE_CARRITO_TO_CARRITO_COMPRA" 
  FOREIGN KEY("ID_CARRITO_COMPRA")
  REFERENCES CARRITO_COMPRA("ID");


  




select * from USUARIOS //Backend
select * from CATEGORIA_PRODUCTO //Backend
select * from PRODUCTO //Backend
select * from CARRITO_COMPRA //Backend
select * from DETALLE_CARRITO




INSERT INTO [dbo].[USUARIOS]([USER_NAME], [NOMBRE_COMPLETO], [PASSWORD]) VALUES ('jorge.c', 'Jorge Campos', '2023') 




INSERT INTO [dbo].[CATEGORIA_PRODUCTO]([NOMBRE]) VALUES ('Limpieza') 


INSERT INTO [dbo].[PRODUCTO]([NOMBRE], [ID_CATEGORIA]) VALUES ('Producto 1', 1) 
INSERT INTO [dbo].[PRODUCTO]([NOMBRE], [ID_CATEGORIA]) VALUES ('Producto 2', 1) 
INSERT INTO [dbo].[PRODUCTO]([NOMBRE], [ID_CATEGORIA]) VALUES ('Producto 3', 1) 



INSERT INTO [dbo].[CARRITO_COMPRA]([FECHA], [ID_USUARIO]) VALUES (getdate(), 1) 


INSERT INTO [dbo].[DETALLE_CARRITO]([CANTIDAD], [ID_PRODUCTO], [ID_CARRITO_COMPRA]) VALUES (10, 1, 1) 
INSERT INTO [dbo].[DETALLE_CARRITO]([CANTIDAD], [ID_PRODUCTO], [ID_CARRITO_COMPRA]) VALUES (5, 2, 1) 
INSERT INTO [dbo].[DETALLE_CARRITO]([CANTIDAD], [ID_PRODUCTO], [ID_CARRITO_COMPRA]) VALUES (1, 3, 1) 


--******************888PROCEDIMEINTO ALMACENADOS*****************

-- LISTAR todos los PROCEDIMIENTOS
SELECT *
FROM sys.procedures

--**************** inicio tabla usuarios *****************
-- Obtener Usurios
CREATE PROCEDURE ObtenerUsuarios
AS
BEGIN
    SELECT * FROM usuarios;
END

-- Insertar Usuarios
CREATE PROCEDURE InsertUsuario
    @user_name NVARCHAR(100),
    @nombre_completo NVARCHAR(100),
    @password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO [dbo].[USUARIOS] ([USER_NAME], [NOMBRE_COMPLETO], [PASSWORD])
    VALUES (@user_name, @nombre_completo, @password);

    SELECT @@ROWCOUNT;
END

-- Obtener Usuarios segun id 
CREATE PROCEDURE ObtenerUsuarioPorId
    @Id INT
AS
BEGIN
    SELECT *
    FROM usuarios
    WHERE ID = @Id AND estado_registro = 1;
END

-- eliminar
CREATE PROCEDURE EliminarUsuarioPorId
    @Id INT
AS
BEGIN
    DELETE FROM usuarios WHERE ID = @Id;
END

-- update
CREATE PROCEDURE UpdateUsuario
    @Id INT,
    @user_name NVARCHAR(100),
    @nombre_completo NVARCHAR(100),
    @password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE [dbo].[USUARIOS]
    SET [USER_NAME] = @user_name,
        [NOMBRE_COMPLETO] = @nombre_completo,
        [PASSWORD] = @password
    WHERE [ID] = @Id;
    SELECT @@ROWCOUNT;
END
--****************** fin Tabla usuarios **********************

--**************** inicio tabla DetalleCarrito *****************
-- delete by id
CREATE PROCEDURE EliminarDetalleCarritoPorId
    @Id INT
AS
BEGIN
    DELETE FROM [dbo].[DETALLE_CARRITO] WHERE [ID] = @Id;
END

-- update 
CREATE PROCEDURE ModificarDetalleCarrito
    @id INT,
    @cantidad INT,
    @id_producto INT,
    @id_carrito_compra INT
AS
BEGIN
    UPDATE [dbo].[DETALLE_CARRITO]
    SET [CANTIDAD] = @cantidad,
        [ID_PRODUCTO] = @id_producto,
        [ID_CARRITO_COMPRA] = @id_carrito_compra
    WHERE [ID] = @id;
END


--**************** fin tabla DetalleCarrito *****************

--**************** inicio tabla CarritoCompra ***************
--insert
CREATE PROCEDURE InsertarCarritoCompra
    @fecha DATE,
    @id_usuario INT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO [dbo].[CARRITO_COMPRA] ([FECHA], [ID_USUARIO])
    VALUES (@fecha, @id_usuario);
    
    SELECT @@ROWCOUNT;
END

--get all
CREATE PROCEDURE ObtenerCarritoCompra
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[CARRITO_COMPRA];
END

--get by id
CREATE PROCEDURE ObtenerCarritoCompraPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM [dbo].[CARRITO_COMPRA]
    WHERE [ID] = @Id AND [estado_registro] = 1;
END

--update 
CREATE PROCEDURE ActualizarCarritoCompra
    @Id INT,
    @fecha DATE,
    @id_usuario INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE [dbo].[CARRITO_COMPRA]
    SET [FECHA] = @fecha,
        [ID_USUARIO] = @id_usuario
    WHERE [ID] = @Id;

    SELECT @@ROWCOUNT;
END

--delete
CREATE PROCEDURE EliminarCarritoCompra
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM [dbo].[CARRITO_COMPRA]
    WHERE [ID] = @Id;

    SELECT @@ROWCOUNT;
END

--****************fin tabla CarritoCompra *******************

--*************** inicio tabla Producto *********************
--insert

--get all

--get by id

--update 

--delete

--*************** fin tabla Producto ************************

--*************** inicio tabla CategoriaProducto ************
--insert

--get all

--get by id

--update 

--delete

--*************** fin tabla CategoriaProducto **************