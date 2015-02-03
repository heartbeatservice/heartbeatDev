CREATE TABLE [dbo].[UserRole]
(
	[UserId] INT NOT NULL , 
    [RoleId] INT NOT NULL, 
    PRIMARY KEY ([UserId], [RoleId]), 
    CONSTRAINT [FK_UserRole_ToUser] FOREIGN KEY ([UserId]) REFERENCES [UserProfile]([UserId]), 
    CONSTRAINT [FK_UserRole_ToRole] FOREIGN KEY ([RoleId]) REFERENCES [Roles]([RoleId])
)
