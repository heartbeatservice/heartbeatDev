CREATE TABLE [dbo].[WebMenu] (
    [MenuId]      INT            IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (100) NOT NULL,
    [Controller]  NVARCHAR (50)  NULL,
    [Action]      NVARCHAR (50)  NULL,
    [OrderNumber] INT            NULL,
    CONSTRAINT [PK_WebMenu] PRIMARY KEY CLUSTERED ([MenuId] ASC)
);

