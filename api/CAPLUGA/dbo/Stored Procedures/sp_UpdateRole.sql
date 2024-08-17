CREATE PROCEDURE sp_UpdateRole
    @RoleID BIGINT,
    @RoleName NVARCHAR(100)
AS
BEGIN
    UPDATE Roles
    SET RoleName = @RoleName
    WHERE RolesID = @RoleID
END