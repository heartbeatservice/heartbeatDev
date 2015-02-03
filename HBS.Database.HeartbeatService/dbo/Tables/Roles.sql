CREATE TABLE [dbo].[Roles] (
    [RoleId]      INT           IDENTITY (1, 1) NOT NULL,
    [CompanyId]   INT           NOT NULL,
    [RoleName]    VARCHAR (150) NOT NULL,
    [CreatedBy]   INT           NULL,
    [CreatedDate] DATETIME      NULL,
    [UpdatedBy]   INT           NULL,
    [DateUpdated] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([RoleId] ASC)
);


