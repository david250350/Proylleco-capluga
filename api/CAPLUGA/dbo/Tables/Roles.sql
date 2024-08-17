CREATE TABLE [dbo].[Roles] (
    [RolesID]  BIGINT        IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([RolesID] ASC)
);

