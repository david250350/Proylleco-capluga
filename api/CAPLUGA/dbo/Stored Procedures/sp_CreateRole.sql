CREATE PROCEDURE sp_CreateRole
    @RoleName NVARCHAR(100)
AS
BEGIN
    INSERT INTO Roles (RoleName)
    VALUES (@RoleName)
END