CREATE TABLE [dbo].[Customers] (
    [CustomerId]    INT            IDENTITY (1, 1) NOT NULL,
    [CompanyID]     INT            NOT NULL,
    [FirstName]     NVARCHAR (50)  NULL,
    [MiddleInitial] VARCHAR (1)    NULL,
    [LastName]      NVARCHAR (50)  NULL,
    [DateOfBirth]   DATE           NULL,
    [Address1]      NVARCHAR (100) NULL,
    [Address2]      NVARCHAR (100) NULL,
    [City]          NVARCHAR (50)  NULL,
    [State]         CHAR (2)       NULL,
    [Zip]           CHAR (10)      NULL,
    [HomePhone]     NVARCHAR (10)  NULL,
    [CellPhone]     NVARCHAR (10)  NULL,
    [Email]         NVARCHAR (150) NULL,
    [CreatedBy]     INT            NULL,
    [DateCreated]   DATETIME       NULL,
    [UpdatedBy]     INT            NULL,
    [DateUpdated]   DATETIME       NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerId] ASC)
);





