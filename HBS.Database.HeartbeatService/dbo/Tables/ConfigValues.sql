CREATE TABLE [dbo].[ConfigValues]
(
	[ConfigId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CompanyId] INT NOT NULL, 
    [ControlKey] VARCHAR(250) NULL, 
    [ControlValue] VARCHAR(500) NULL, 
    [ControlOrder] FLOAT NULL, 
    [ControlItemKey] VARCHAR(250) NULL, 
    [ControlItemValue] VARCHAR(250) NULL, 
    [ControlItemText] VARCHAR(250) NULL, 
    [ControlItemOrder] FLOAT NULL, 
    [OtherText] VARCHAR(250) NULL, 
    CONSTRAINT [FK_ConfigValues_ToCompany] FOREIGN KEY ([CompanyId]) REFERENCES [Company]([CompanyId]) 
)
