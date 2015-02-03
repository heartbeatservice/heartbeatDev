
CREATE PROCEDURE [dbo].[UpdateUser] 
--UpdateUser 1,1,'ABCTesting','','Umais','Siddiqui','umais@heartbeatservice.com'

@UserId int,
@CompanyId int=NULL,
@Password nvarchar(128)=NULL,
@PasswordSalt nvarchar(128)=NULL,
@FirstName nvarchar(100)=NULL,
@LastName nvarchar(100)=NULL,
@Email nvarchar(150)=NULL,
@ConfirmationToken nvarchar(128)=NULL,
@IsConfirmed bit=NULL,
@CreatedBy int=NULL,
@UpdatedBy int=NULL,
@IsActive bit=NULL


AS



DECLARE @UpdatedDate DATETime=GetUtcDate();

declare @sql varchar(256)
Declare @tblParams  table(id int identity(1,1),name varchar(50),paramName varchar(50))

SET @Sql='Update  UserProfile
Set'

If(@CompanyId is not Null)
Insert Into @tblParams Values('CompanyId',@CompanyId)



If(@FirstName is not Null)
Insert Into @tblParams Values('FirstName',@FirstName)

If(@LastName is not Null)
Insert Into @tblParams Values('LastName',@LastName)



If(@Password is not Null)
Insert Into @tblParams Values('Password',CONVERT(varchar,@Password))

If(@PasswordSalt is not Null)
Insert Into @tblParams Values('PasswordSalt',CONVERT(varchar,@PasswordSalt))


If(@Email is not Null)
Insert Into @tblParams Values('Email',CONVERT(varchar,@Email))

If(@ConfirmationToken is not Null)
Insert Into @tblParams Values('ConfirmationToken',CONVERT(varchar,@ConfirmationToken))

If(@IsConfirmed is not Null)
Insert Into @tblParams Values('IsConfirmed',CONVERT(varchar,@IsConfirmed))


If(@UpdatedDate is not Null)
Insert Into @tblParams Values('UpdatedDate',CONVERT(varchar,@UpdatedDate))

If(@UpdatedBy is not Null)
Insert Into @tblParams Values('UpdatedBy',CONVERT(varchar,@UpdatedBy))

If(@IsActive is not Null)
Insert Into @tblParams Values('IsActive',CONVERT(varchar,@IsActive))


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

Set @SQL=@Sql+' WHERE UserId='+CONVERT(varchar,@UserId) 
EXECute(@sql)