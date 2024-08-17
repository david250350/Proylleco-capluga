CREATE TABLE [dbo].[Cart] (
    [CartID]              BIGINT IDENTITY (1, 1) NOT NULL,
    [MedicalImplementsID] BIGINT NOT NULL,
    [Quantity]            INT    NOT NULL,
    [UserID]              BIGINT NOT NULL,
    CONSTRAINT [PK_Cart] PRIMARY KEY CLUSTERED ([CartID] ASC),
    FOREIGN KEY ([MedicalImplementsID]) REFERENCES [dbo].[MedicalImplements] ([MedicalImplementsID]),
    CONSTRAINT [FK_Cart_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

