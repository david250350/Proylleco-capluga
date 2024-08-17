CREATE TABLE [dbo].[MasterPurchaseCurse] (
    [MasterPurchaseCurseID] BIGINT          IDENTITY (1, 1) NOT NULL,
    [UserID]                BIGINT          NOT NULL,
    [PurchaseDate]          DATETIME        NOT NULL,
    [TotalPurchase]         DECIMAL (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([MasterPurchaseCurseID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

