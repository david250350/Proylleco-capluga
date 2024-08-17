
CREATE PROCEDURE [dbo].[UpdateAppointment]
    @AppointmentID BIGINT,
    @UserID BIGINT,
    @AddressID BIGINT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(MAX),
	 @ScheduleID BIGINT
AS
BEGIN
    UPDATE dbo.AppointmentScheduling
    SET Name = @Name,
        Description = @Description,
		ScheduleID = @ScheduleID
    WHERE AppointmentID = @AppointmentID;
END