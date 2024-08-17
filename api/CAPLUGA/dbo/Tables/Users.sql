CREATE TABLE [dbo].[Users] (
    [UserID]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [UserName]         VARCHAR (50)  NOT NULL,
    [Surnames]         VARCHAR (50)  NOT NULL,
    [Email]            VARCHAR (100) NOT NULL,
    [Password]         VARCHAR (255) NOT NULL,
    [RegistrationDate] DATETIME      NOT NULL,
    [State]            BIT           NOT NULL,
    [Age]              DATE          NOT NULL,
    [PhoneNumber]      VARCHAR (50)  NOT NULL,
    [RolesID]          BIGINT        NOT NULL,
    [AddressID]        BIGINT        NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    FOREIGN KEY ([RolesID]) REFERENCES [dbo].[Roles] ([RolesID]),
    CONSTRAINT [FK_Users_Addresses] FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([AddressID])
);

