
CREATE PROCEDURE [dbo].[UpdateRol]
    @RolesID BIGINT,
    @RoleName NVARCHAR(100)
AS
BEGIN

        UPDATE dbo.Roles
        SET RoleName = @RoleName
           
        WHERE RolesID = @RolesID;
    END