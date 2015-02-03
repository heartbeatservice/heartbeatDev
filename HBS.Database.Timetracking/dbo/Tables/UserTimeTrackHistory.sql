CREATE TABLE [dbo].[UserTimeTrackHistory] (
    [TimeTrackId]  INT              IDENTITY (1, 1) NOT NULL,
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [UserName]     NVARCHAR (50)    NOT NULL,
    [ClockInTime]  VARCHAR (50)     NULL,
    [ClockOutTime] VARCHAR (50)     NULL,
    [StampDate]    DATETIME         NOT NULL,
    [UserIP]       VARCHAR (20)     NULL,
    [IsDeleted]    BIT              CONSTRAINT [DF_UserTimeTrackHistory_IsDeleted] DEFAULT ((0)) NOT NULL,
    [CreatedDate]  DATETIME         NULL,
    [CreatedBy]    VARCHAR (50)     NULL,
    [UpdatedDate]  DATETIME         NULL,
    [UpdatedBy]    VARCHAR (50)     NULL,
    CONSTRAINT [PK_UserTimeStampHistory] PRIMARY KEY CLUSTERED ([TimeTrackId] ASC),
    CONSTRAINT [FK_UserTimeStampHistory_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

