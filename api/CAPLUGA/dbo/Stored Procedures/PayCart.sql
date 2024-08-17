CREATE PROCEDURE [dbo].[PayCart]
    @UserID BIGINT
AS
BEGIN

IF (SELECT	TOP 1 P.Quantity - C.Quantity 
		FROM	MedicalImplements	P
		INNER	JOIN	Cart  C	ON P.MedicalImplementsID = C.MedicalImplementsID
		WHERE	UserID = @UserID) < 0
	BEGIN

		SELECT 'FALSE'
		
	END
	ELSE
	BEGIN

    DECLARE @TotalPurchase DECIMAL(18,2);
    DECLARE @MasterCode BIGINT;
    
    SELECT @TotalPurchase = SUM(P.Price * C.Quantity) + (SUM(P.Price * C.Quantity) * 0.13)
    FROM Cart C
    INNER JOIN MedicalImplements P ON C.MedicalImplementsID = P.MedicalImplementsID
    WHERE C.UserID = @UserID;

    INSERT INTO dbo.MasterPurchase(UserID, PurchaseDate, TotalPurchase)
    VALUES (@UserID, GETDATE(), @TotalPurchase);

    SET @MasterCode = SCOPE_IDENTITY();

    INSERT INTO dbo.Detail(MasterPurchaseID, MedicalImplementsID, PaidPrice, PaidQuantity, Tax, PaymentStatus)
    SELECT @MasterCode, C.MedicalImplementsID, P.Price, C.Quantity, P.Price * 0.13, 'PENDIENTE'
    FROM Cart C
    INNER JOIN MedicalImplements P ON C.MedicalImplementsID = P.MedicalImplementsID
    WHERE C.UserID = @UserID;

    DELETE FROM Cart WHERE UserID = @UserID;

    SELECT 'TRUE'
END
END