CREATE TABLE [dbo].[Detail] (
    [DetailID]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [MedicalImplementsID] BIGINT          NOT NULL,
    [PaidPrice]           DECIMAL (18, 2) NOT NULL,
    [PaidQuantity]        INT             NOT NULL,
    [Tax]                 DECIMAL (18, 2) NOT NULL,
    [MasterPurchaseID]    BIGINT          NOT NULL,
    [PaymentStatus]       NVARCHAR (20)   DEFAULT ('PENDIENTE') NULL,
    PRIMARY KEY CLUSTERED ([DetailID] ASC),
    FOREIGN KEY ([MasterPurchaseID]) REFERENCES [dbo].[MasterPurchase] ([MasterPurchaseID]),
    FOREIGN KEY ([MedicalImplementsID]) REFERENCES [dbo].[MedicalImplements] ([MedicalImplementsID])
);

