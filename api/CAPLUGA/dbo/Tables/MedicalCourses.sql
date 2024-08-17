CREATE TABLE [dbo].[MedicalCourses] (
    [MedicalCourseID] BIGINT          IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (255)  NOT NULL,
    [Description]     NVARCHAR (MAX)  NOT NULL,
    [Quantity]        INT             NOT NULL,
    [Price]           DECIMAL (18, 2) NOT NULL,
    [Image]           NVARCHAR (MAX)  NOT NULL,
    [State]           BIT             NOT NULL,
    [DateandTime]     DATETIME        NULL,
    PRIMARY KEY CLUSTERED ([MedicalCourseID] ASC)
);

