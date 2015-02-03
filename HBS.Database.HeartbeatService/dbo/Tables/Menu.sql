CREATE TABLE [dbo].[Menu] (
    [MenuId]      INT            IDENTITY (1, 1) NOT NULL,
    [CompanyId] INT NULL,
	[Title]       NVARCHAR (100) NOT NULL,
    [Controller]  NVARCHAR (50)  NULL,
    [Action]      NVARCHAR (50)  NULL,
    [OrderNumber] INT            NULL,
    CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED ([MenuId] ASC), 
    CONSTRAINT [FK_Menu_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId])
);