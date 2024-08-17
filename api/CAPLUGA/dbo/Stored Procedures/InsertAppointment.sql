CREATE PROCEDURE [dbo].[InsertAppointment]
    @UserID BIGINT,
    @AddressID BIGINT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(MAX),
@ScheduleID BIGINT
AS
BEGIN
    INSERT INTO AppointmentScheduling (UserID, AddressID, Name, Description, ScheduleID)
    VALUES (@UserID, @AddressID, @Name, @Description, @ScheduleID)
    
 
END