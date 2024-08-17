CREATE PROCEDURE [dbo].[InsertRol]
    -- Parámetros para el procedimiento almacenado
    @RoleName NVARCHAR(100)
   
AS
BEGIN
    -- Insertar el nuevo registro en la tabla de diseño total
    INSERT INTO Roles(RoleName)
    VALUES (@RoleName)
   
END