CREATE PROCEDURE [dbo].[ApprovePaymentTuition]
    @MasterPurchaseCurseID BIGINT
AS
BEGIN
    UPDATE DetailCurse
    SET PaymentStatus = 'APROBADO'
    WHERE MasterPurchaseCurseID = @MasterPurchaseCurseID AND PaymentStatus = 'PENDIENTE';
    
    UPDATE MedicalCourses
    SET Quantity = Quantity - D.PaidQuantity
    FROM MedicalCourses M
    INNER JOIN DetailCurse D ON M.MedicalCourseID = D.MedicalCourseID
    WHERE D.MasterPurchaseCurseID = MasterPurchaseCurseID AND D.PaymentStatus = 'APROBADO';

END