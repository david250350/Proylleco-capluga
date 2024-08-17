CREATE TABLE [dbo].[ScheduleAppointment] (
    [ScheduleID]  BIGINT         IDENTITY (1, 1) NOT NULL,
    [Dname]       NVARCHAR (100) NOT NULL,
    [DateandTime] DATETIME       NOT NULL,
    [IsBooked]    BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ScheduleID] ASC)
);

