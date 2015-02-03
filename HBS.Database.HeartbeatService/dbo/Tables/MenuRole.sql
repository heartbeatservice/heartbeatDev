CREATE TABLE [dbo].[MenuRole]
(
	[MenuId] INT NOT NULL , 
    [RoleId] INT NOT NULL, 
    PRIMARY KEY ([MenuId], [RoleId]), 
	CONSTRAINT [FK_MenuRole_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]),
    CONSTRAINT [FK_MenuRole_Menu] FOREIGN KEY ([MenuId]) REFERENCES [dbo].[Menu] ([MenuId])

)
