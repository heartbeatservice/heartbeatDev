CREATE TABLE [dbo].[Professional] (
    [ProfessionalId]                   INT            IDENTITY (1, 1) NOT NULL,
    [ProfessionalTypeId]               INT            NOT NULL,
    [CompanyId]                        INT            NOT NULL,
    [Title]                            NVARCHAR (50)  NULL,
    [FirstName]                        NVARCHAR (100) NULL,
    [MiddleInitial]                    NVARCHAR (1)   NULL,
    [LastName]                         NVARCHAR (100) NULL,
    [Phone]                            NVARCHAR (50)  NULL,
    [ProfessionalIdentificationNumber] NVARCHAR (150) NULL,
    [Email]                            NVARCHAR (150) NULL,
    [IsActive]                         INT            CONSTRAINT [DF_Professional_IsActive] DEFAULT ((1)) NOT NULL,
    [CreatedBy]                        INT            NULL,
    [CreatedDate]                      DATETIME       NULL,
    [UpdatedBy]                        INT            NULL,
    [UpdatedDate]                      DATETIME       NULL,
    CONSTRAINT [PK_Professional] PRIMARY KEY CLUSTERED ([ProfessionalId] ASC),
    CONSTRAINT [FK_Professional_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);







