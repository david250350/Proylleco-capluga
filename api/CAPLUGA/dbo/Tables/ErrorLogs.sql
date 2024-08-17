CREATE TABLE [dbo].[ErrorLogs] (
    [LogID]                 BIGINT         IDENTITY (1, 1) NOT NULL,
    [Timestamp]             DATETIME       NOT NULL,
    [ErrorMessage]          VARCHAR (MAX)  NOT NULL,
    [Source]                VARCHAR (255)  NOT NULL,
    [AdditionalInformation] NVARCHAR (MAX) NOT NULL,
    [UserID]                BIGINT         NOT NULL,
    PRIMARY KEY CLUSTERED ([LogID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

