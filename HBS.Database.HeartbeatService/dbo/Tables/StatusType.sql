CREATE TABLE [dbo].[StatusType] (
    [StatusId]   INT            IDENTITY (1, 1) NOT NULL,
    [StatusDesc] NVARCHAR (150) NULL,
    [CompanyId]  INT            NOT NULL,
    CONSTRAINT [PK_StatusType] PRIMARY KEY CLUSTERED ([StatusId] ASC), 
    CONSTRAINT [FK_StatusType_Company] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId])
);

