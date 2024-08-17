CREATE TABLE [dbo].[Registeredcourses] (
    [RegisteredcoursesID] BIGINT   IDENTITY (1, 1) NOT NULL,
    [UserID]              BIGINT   NOT NULL,
    [MedicalCourseID]     BIGINT   NOT NULL,
    [Quantity]            INT      NOT NULL,
    [Registrationdate]    DATETIME NOT NULL,
    CONSTRAINT [PK_Registeredcourses] PRIMARY KEY CLUSTERED ([RegisteredcoursesID] ASC),
    CONSTRAINT [FK_Registeredcourses_MedicalCourses] FOREIGN KEY ([MedicalCourseID]) REFERENCES [dbo].[MedicalCourses] ([MedicalCourseID]),
    CONSTRAINT [FK_Registeredcourses_Users] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
);

