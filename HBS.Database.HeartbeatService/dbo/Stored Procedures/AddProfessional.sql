
CREATE PROCEDURE [dbo].[AddProfessional]

@ProfessionalTypeId int,
@CompanyId int,
@Title nvarchar(50)=NULL,
@FirstName nvarchar(100)=NULL,
@MiddleInitial nvarchar(1)=NULL,
@LastName nvarchar(100)=NULL,
@Phone nvarchar(50)=NULL,
@ProfessionalIdentificationNumber nvarchar(50)=NULL,
@Email nvarchar(150)=NULL,
@CreatedBy int=NULL

AS


INSERT INTO Professional(ProfessionalTypeId, CompanyId,Title,FirstName,MiddleInitial,LastName,Phone,ProfessionalIdentificationNumber,Email,CreatedBy,CreatedDate) 
VALUES(@ProfessionalTypeId,@companyId,@Title,@FirstName,@MiddleInitial,@LastName,@Phone,@ProfessionalIdentificationNumber,@Email,@CreatedBy,GetUtcDate())