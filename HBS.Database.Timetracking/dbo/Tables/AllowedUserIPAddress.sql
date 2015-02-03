CREATE TABLE [dbo].[AllowedUserIPAddress] (
    [UserId]      UNIQUEIDENTIFIER NOT NULL,
    [UserName]    NVARCHAR (50)    NULL,
    [IPAddress]   VARCHAR (20)     NULL,
    [IsDeleted]   BIT              NULL,
    [CreatedBy]   VARCHAR (50)     NULL,
    [CreatedDate] DATETIME         NULL,
    [UpdateBy]    VARCHAR (50)     NULL,
    [UpdateDate]  DATETIME         NULL
);

