CREATE PROCEDURE [dbo].[tuition]
    @UserID BIGINT
AS
BEGIN

IF (SELECT	TOP 1 P.Quantity - C.Quantity 
		FROM	MedicalCourses	P
		INNER	JOIN	Registeredcourses  C	ON P.MedicalCourseID = C.MedicalCourseID
		WHERE	UserID = @UserID) < 0
	BEGIN

		SELECT 'FALSE'
		
	END
	ELSE
	BEGIN

    DECLARE @TotalPurchase DECIMAL(18,2);
    DECLARE @MasterCode BIGINT;
    
    SELECT @TotalPurchase = SUM(P.Price * C.Quantity) + (SUM(P.Price * C.Quantity) * 0.13)
    FROM Registeredcourses C
    INNER JOIN MedicalCourses P ON C.MedicalCourseID = P.MedicalCourseID
    WHERE C.UserID = @UserID;

    INSERT INTO dbo.MasterPurchaseCurse(UserID, PurchaseDate, TotalPurchase)
    VALUES (@UserID, GETDATE(), @TotalPurchase);

    SET @MasterCode = SCOPE_IDENTITY();

    INSERT INTO dbo.DetailCurse(MasterPurchaseCurseID, MedicalCourseID, PaidPrice, PaidQuantity, Tax, PaymentStatus)
    SELECT @MasterCode, C.MedicalCourseID, P.Price, C.Quantity, P.Price * 0.13, 'PENDIENTE'
    FROM Registeredcourses C
    INNER JOIN MedicalCourses P ON C.MedicalCourseID = P.MedicalCourseID
    WHERE C.UserID = @UserID;

    DELETE FROM Registeredcourses WHERE UserID = @UserID;

    SELECT 'TRUE'
END
END