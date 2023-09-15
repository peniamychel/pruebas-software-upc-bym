
--  /*************************PROCEDIMEINTO ALMACENADOS****************\
-- /                                                                    \
--/                                                                      \
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

--*****************Buscar segun usuario y contrasenia retorna id *******
CREATE PROCEDURE BuscarUsuarioContrasenia
    @UserName VARCHAR(40),
    @Password VARCHAR(100),
    @ID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT @ID = ID
    FROM USUARIOS
    WHERE USER_NAME = @UserName AND PASSWORD = @Password;

    IF (@@ROWCOUNT = 0)
        SET @ID = 0;
END

--********************* Buscar segun nombre y contrasenia y devuelve todos los sus datos *****
CREATE PROCEDURE SearchUserByUserAndPass
    @UserName VARCHAR(40),
    @Password VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM USUARIOS
    WHERE USER_NAME = @UserName AND PASSWORD = @Password AND ESTADO_REGISTRO = 1;
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

--devuelve el id con el que se registro
CREATE PROCEDURE InsertCarritoCompraReturnId2
  @Fecha datetime,
  @IdUsuario int,
  @IdCarrito int OUTPUT
AS
BEGIN
  
  INSERT INTO CARRITO_COMPRA
    (FECHA, ID_USUARIO)
  VALUES
    (@Fecha, @IdUsuario)

  SET @IdCarrito = SCOPE_IDENTITY()

END


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
CREATE PROCEDURE ActualizarProducto
    @ID INT,
    @NOMBRE VARCHAR(100),
    @ID_CATEGORIA INT
AS
BEGIN
    UPDATE PRODUCTO
    SET NOMBRE = @NOMBRE,
        ID_CATEGORIA = @ID_CATEGORIA
    WHERE ID = @ID;
END;
--delete
CREATE PROCEDURE EliminarProducto
    @ID INT
AS
BEGIN
    DELETE FROM PRODUCTO
    WHERE ID = @ID;
END;
--*************** fin tabla Producto ************************

--*************** inicio tabla CategoriaProducto ************
--insert

--get all

--get by id

--update 

--delete

--*************** fin tabla CategoriaProducto **************
--*************** NINIO ************
--insert
CREATE PROCEDURE InsertarNinio
    @NOMBRE VARCHAR(100),
    @EDAD INT,
    @SEXO VARCHAR(20)
AS
BEGIN
    INSERT INTO NINIO ("NOMBRE", "EDAD", "SEXO")
    VALUES (@NOMBRE, @EDAD, @SEXO);
END;

--get LOS 10 ULTIMOS REGISTROS
CREATE PROCEDURE Obtener_Ultimos_Registros_Ninio
AS
BEGIN
    SELECT TOP 10 *
    FROM NINIO
    WHERE ESTADO_REGISTRO = 1
    ORDER BY FECHA_REGISTRO DESC;
END;

--get by id
CREATE PROCEDURE ObtenerNinioPorID
  @ID INT
AS
BEGIN
  SELECT *
  FROM NINIO
  WHERE ID = @ID;
END;

--update 
CREATE PROCEDURE EditarNinio
(
  @ID INT,
  @NOMBRE VARCHAR(100),
  @EDAD INT,
  @SEXO VARCHAR(20),
  @USUARIO_REGISTRO VARCHAR(50),
  @FECHA_REGISTRO DATETIME,
  @ESTADO_REGISTRO INT
)
AS
BEGIN
  UPDATE NINIO
  SET
    NOMBRE = @NOMBRE,
    EDAD = @EDAD,
    SEXO = @SEXO,
    USUARIO_REGISTRO = @USUARIO_REGISTRO,
    FECHA_REGISTRO = @FECHA_REGISTRO,
    ESTADO_REGISTRO = @ESTADO_REGISTRO
  WHERE ID = @ID;
END;

--delete (solo se desabilita el ESTADO_REGISTRO)
CREATE PROCEDURE CambiarEstadoRegistroCero
  @ID INT
AS
BEGIN
  UPDATE NINIO
  SET ESTADO_REGISTRO = 0
  WHERE ID = @ID;
END;

--*************** fin NINIO **************
--*************** DEJA_EN_GUARDERIA ************
--insert
CREATE PROCEDURE insertaDejaEnGuarderia
(
@IdUsuario INT,
@IdNino INT
)
AS
BEGIN
INSERT INTO DEJA_EN_GUARDERIA (ID_USUARIO, ID_NINIO, HORA_ENTRADA)
VALUES (@IdUsuario, @IdNino, GETDATE())
END

--get los 10 ultimos
CREATE PROCEDURE getEndRegistros
AS
BEGIN
    SELECT TOP 10 *
    FROM DEJA_EN_GUARDERIA
    ORDER BY FECHA_REGISTRO DESC;
END;

--get by id

--update 

--delete

--acutlaizar hora salida
CREATE PROCEDURE ACTUALIZAR_HORA_SALIDA
    @ID INT
AS
BEGIN
    UPDATE DEJA_EN_GUARDERIA
    SET HORA_SALIDA = CONVERT(TIME, GETDATE())
    WHERE ID = @ID;
END;
--se usa 
EXEC ACTUALIZAR_HORA_SALIDA @ID = 1;

--*************** fin DEJA_EN_GUARDERIA **************

