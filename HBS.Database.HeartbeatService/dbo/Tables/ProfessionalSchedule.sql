CREATE TABLE [dbo].[ProfessionalSchedule] (
    [ProfessionalScheduleId] INT      IDENTITY (1, 1) NOT NULL,
    [ProfessionalId]         INT      NOT NULL,
    [StartTime]              DATETIME NOT NULL,
    [EndTime]                DATETIME NULL,
    [TimeIntervalMinutes]    SMALLINT NULL,
    CONSTRAINT [PK_ProfessionalSchedule] PRIMARY KEY CLUSTERED ([ProfessionalScheduleId] ASC)
);

