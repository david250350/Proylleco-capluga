﻿CREATE PROCEDURE sp_DeleteRole
    @RoleID BIGINT
AS
BEGIN
    DELETE FROM Roles
    WHERE RolesID = @RoleID
END