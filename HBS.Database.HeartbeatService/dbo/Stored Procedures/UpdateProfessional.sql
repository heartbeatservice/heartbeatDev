
CREATE PROCEDURE [dbo].[UpdateProfessional] 
--UpdateProfessional 1,Null,Null,'Madame',Null,Null,Null,Null,Null,Null,Null,2

@ProfessionalId int,
@ProfessionalTypeId int=NULL,
@CompanyId int=NULL,
@Title nvarchar(50)=NULL,
@FirstName nvarchar(100)=NULL,
@MiddleIntial nvarchar(1)=NULL,
@LastName nvarchar(100)=NULL,
@Phone nvarchar(50)=NULL,
@ProfessionalIdentificationNumber nvarchar(150)=NULL,
@Email nvarchar(150)=NULL,
@IsActive bit=NULL,
@UpdatedBy int=Null

AS

DECLARE @UpdatedDate DATETime=GetUtcDate();

declare @sql varchar(256)
Declare @tblParams  table(id int identity(1,1),name varchar(50),paramName varchar(50))

SET @Sql='Update Professional
Set'

If(@ProfessionalTypeId is not Null)
Insert Into @tblParams Values('ProfessionalTypeId',@ProfessionalTypeId)


If(@CompanyId is not Null)
Insert Into @tblParams Values('CompanyId',@CompanyId)

If(@Title is not Null)
Insert Into @tblParams Values('Title',@Title)


If(@FirstName is not Null)
Insert Into @tblParams Values('FirstName',@FirstName)

If(@MiddleIntial is not Null)
Insert Into @tblParams Values('MiddleIntial',@MiddleIntial)

If(@LastName is not Null)
Insert Into @tblParams Values('LastName',@LastName)

If(@Phone is not Null)
Insert Into @tblParams Values('Phone',CONVERT(varchar,@Phone))

If(@ProfessionalIdentificationNumber is not Null)
Insert Into @tblParams Values('ProfessionalIdentificationNumber',CONVERT(varchar,@ProfessionalIdentificationNumber))

If(@Email is not Null)
Insert Into @tblParams Values('Email',CONVERT(varchar,@Email))

If(@IsActive is not Null)
Insert Into @tblParams Values('IsActive',@IsActive)

If(@UpdatedBy is not Null)
Insert Into @tblParams Values('UpdatedBy',@UpdatedBy)

If(@UpdatedDate is not Null)
Insert Into @tblParams Values('UpdatedDate',CONVERT(varchar,@UpdatedDate))

Declare @start int,@end int
Declare @UpdateColumn varchar(50)
Select @start =min(Id) from @tblParams
Select @end=max(Id) from @tblParams

While @start<=@end

BEGIN
Select @UpdateColumn=name+'='''+paramName+'''' FROM @tblParams WHERE ID=@start
if @start=1
Set @sql=@sql+ ' '+@UpdateColumn
ELSE
Set @sql=@sql+',' +@updateColumn
SET @start=@start+1
END

Set @SQL=@Sql+' WHERE ProfessionalID='+CONVERT(varchar,@ProfessionalID) 
EXECute(@sql)