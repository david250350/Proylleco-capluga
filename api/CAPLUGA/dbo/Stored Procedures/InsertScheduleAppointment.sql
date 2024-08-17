CREATE PROCEDURE [dbo].[InsertScheduleAppointment]
    -- Parámetros para el procedimiento almacenado
    @Dname NVARCHAR(100),
    @DateandTime DATETIME
AS
BEGIN
    -- Insertar el nuevo registro en la tabla
    INSERT INTO ScheduleAppointment (Dname, DateandTime)
    VALUES (@Dname, @DateandTime)
   
END