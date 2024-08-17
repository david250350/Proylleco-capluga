CREATE PROCEDURE [dbo].[UpdateScheduleAppointment]
    @ScheduleID BIGINT,
    @DName NVARCHAR(100),
    @DateandTime DATETIME
AS
BEGIN

        UPDATE dbo.ScheduleAppointment
        SET Dname = @DName,
           DateandTime = @DateandTime
        WHERE ScheduleID = @ScheduleID;
    END