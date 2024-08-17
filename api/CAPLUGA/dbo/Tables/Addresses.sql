CREATE TABLE [dbo].[Addresses] (
    [AddressID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [Street]    NVARCHAR (100) NOT NULL,
    [City]      NVARCHAR (50)  NOT NULL,
    [State]     NVARCHAR (50)  NOT NULL,
    [ZipCode]   NVARCHAR (10)  NOT NULL,
    [Country]   NVARCHAR (50)  NOT NULL,
    [District]  VARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([AddressID] ASC)
);

