CREATE TABLE [dbo].[Appointments] (
    [AppointmentId]        INT             IDENTITY (1, 1) NOT NULL,
    [ProfessionalId]       INT             NOT NULL,
    [CustomerId]           INT             NULL,
    [AppointmentDate]      DATE            NOT NULL,
    [AppointmentStartTime] NVARCHAR (50)   NOT NULL,
    [StatusId]             INT             NULL,
    [Comments]             NVARCHAR (1000) NULL,	
    [CreatedBy] INT NULL, 
    [DateCreated] DATETIME NULL, 
    [UpdatedBy] INT NULL, 
    [DateUpdated] DATETIME NULL, 
    CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([AppointmentId] ASC)
);

