CREATE TABLE [dbo].[Company] (
    [CompanyId]   INT            IDENTITY (1, 1) NOT NULL,
    [CompanyName] VARCHAR (150)  NOT NULL,
    [Description] VARCHAR (1000) NULL,
    [CreatedBy]   INT            NULL,
    [CreatedDate] DATETIME       NULL,
    [UpdatedBy]   INT            NULL,
    [UpdatedDate] DATETIME       NULL,
    [IsActive]    BIT            CONSTRAINT [DF_Company_IsActive] DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([CompanyId] ASC)
);




