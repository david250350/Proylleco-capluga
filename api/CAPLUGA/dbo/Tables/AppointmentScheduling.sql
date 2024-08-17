CREATE TABLE [dbo].[AppointmentScheduling] (
    [AppointmentID] BIGINT         IDENTITY (1, 1) NOT NULL,
    [UserID]        BIGINT         NOT NULL,
    [AddressID]     BIGINT         NOT NULL,
    [Name]          NVARCHAR (100) NOT NULL,
    [Description]   NVARCHAR (MAX) NOT NULL,
    [ScheduleID]    BIGINT         NOT NULL,
    PRIMARY KEY CLUSTERED ([AppointmentID] ASC),
    FOREIGN KEY ([AddressID]) REFERENCES [dbo].[Addresses] ([AddressID]),
    FOREIGN KEY ([ScheduleID]) REFERENCES [dbo].[ScheduleAppointment] ([ScheduleID]),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

