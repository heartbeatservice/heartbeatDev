
CREATE PROCEDURE [dbo].[AddCompany]


@CompanyName nvarchar(150),
@Description nvarchar(1000),

@CreatedBy int=NULL,
@UpdatedBy int=NULL



AS


INSERT INTO Company(CompanyName,Description,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) 
VALUES(@CompanyName,@Description,GetUtcDate(),@CreatedBy,GetUtcDate(),@UpdatedBy)