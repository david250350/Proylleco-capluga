CREATE PROCEDURE [dbo].[ApprovePaymentDetails]
    @MasterPurchaseID BIGINT
AS
BEGIN
    UPDATE Detail
    SET PaymentStatus = 'APROBADO'
    WHERE MasterPurchaseID = @MasterPurchaseID AND PaymentStatus = 'PENDIENTE';
    
    UPDATE MedicalImplements
    SET Quantity = Quantity - D.PaidQuantity
    FROM MedicalImplements M
    INNER JOIN Detail D ON M.MedicalImplementsID = D.MedicalImplementsID
    WHERE D.MasterPurchaseID = @MasterPurchaseID AND D.PaymentStatus = 'APROBADO';

END