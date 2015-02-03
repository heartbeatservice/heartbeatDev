CREATE TABLE [dbo].[ExtendedUserProfile] (
    [UserId]       UNIQUEIDENTIFIER NOT NULL,
    [UserName]     NVARCHAR (50)    NOT NULL,
    [FirstName]    VARCHAR (150)    NULL,
    [LastName]     VARCHAR (150)    NULL,
    [TempPassword] VARCHAR (50)     NULL,
    [Title]        VARCHAR (150)    NULL,
    [HourlyRate]   FLOAT (53)       NULL,
    [Address]      VARCHAR (200)    NULL,
    [City]         VARCHAR (100)    NULL,
    [State]        VARCHAR (10)     NULL,
    [Zip]          VARCHAR (10)     NULL,
    [Phone]        VARCHAR (15)     NULL,
    [CreatedDate]  DATETIME         NULL,
    [CreatedBy]    VARCHAR (50)     NULL,
    [UpdatedDate]  DATETIME         NULL,
    [UpdatedBy]    VARCHAR (50)     NULL,
    CONSTRAINT [PK_ExtendedUserProfile] PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_ExtendedUserProfile_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId])
);

