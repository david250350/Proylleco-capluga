CREATE TABLE [dbo].[DetailCurse] (
    [DetailCurseID]         BIGINT          IDENTITY (1, 1) NOT NULL,
    [MedicalCourseID]       BIGINT          NOT NULL,
    [PaidPrice]             DECIMAL (18, 2) NOT NULL,
    [PaidQuantity]          INT             NOT NULL,
    [Tax]                   DECIMAL (18, 2) NOT NULL,
    [MasterPurchaseCurseID] BIGINT          NOT NULL,
    [PaymentStatus]         NVARCHAR (20)   DEFAULT ('PENDIENTE') NULL,
    PRIMARY KEY CLUSTERED ([DetailCurseID] ASC),
    FOREIGN KEY ([MasterPurchaseCurseID]) REFERENCES [dbo].[MasterPurchaseCurse] ([MasterPurchaseCurseID]),
    FOREIGN KEY ([MedicalCourseID]) REFERENCES [dbo].[MedicalCourses] ([MedicalCourseID])
);

