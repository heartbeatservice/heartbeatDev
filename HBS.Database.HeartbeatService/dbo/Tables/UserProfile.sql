CREATE TABLE [dbo].[UserProfile] (
    [UserId]            INT            IDENTITY (1, 1) NOT NULL,
    [CompanyId]         INT            NOT NULL,
    [UserName]          NVARCHAR (50)  NOT NULL,
    [Password]          NVARCHAR (128) NOT NULL,
    [PasswordSalt]      NVARCHAR (128) NULL,
    [FirstName]         NVARCHAR (100) NULL,
    [LastName]          NVARCHAR (100) NULL,
    [Email]             NVARCHAR (150) NULL,
    [ConfirmationToken] NVARCHAR (128) NULL,
    [IsConfirmed]       BIT            NULL,
    [CreatedDate]       DATETIME       NULL,
    [CreatedBy]         INT            NULL,
    [UpdatedDate]       DATETIME       NULL,
    [UpdatedBy]         INT            NULL,
    [IsActive]          BIT            DEFAULT ('1') NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC),
    CONSTRAINT [FK_UserProfile_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);





