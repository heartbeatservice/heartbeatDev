CREATE TABLE [dbo].[WebMenuRole] (
    [WebMenuId] INT              NOT NULL,
    [RoleId]    UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_WebMenuRole] PRIMARY KEY CLUSTERED ([WebMenuId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_WebMenuRole_aspnet_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId]),
    CONSTRAINT [FK_WebMenuRole_WebMenu] FOREIGN KEY ([WebMenuId]) REFERENCES [dbo].[WebMenu] ([MenuId])
);

