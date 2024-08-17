CREATE TABLE [dbo].[MedicalImplements] (
    [MedicalImplementsID] BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]                VARCHAR (50)    NOT NULL,
    [Description]         NVARCHAR (MAX)  NOT NULL,
    [State]               BIT             NOT NULL,
    [Price]               DECIMAL (18, 2) NOT NULL,
    [Quantity]            INT             NOT NULL,
    [Image]               NVARCHAR (MAX)  NOT NULL,
    PRIMARY KEY CLUSTERED ([MedicalImplementsID] ASC)
);

