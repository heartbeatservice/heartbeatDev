

CREATE PROCEDURE [dbo].[UpdateCompany] 
-- UpdateCompany 2,'Meachem',Null,1,1

@CompanyId int,
@CompanyName nvarchar(150)=NULL,
@Description nvarchar(1000)=NULL,
@UpdatedBy int=NULL,
@IsActive bit=NULL


AS



DECLARE @UpdatedDate DATETime=GetUtcDate();

declare @sql varchar(256)
Declare @tblParams  table(id int identity(1,1),name varchar(50),paramName varchar(50))

SET @Sql='Update  Company
Set'



If(@CompanyName is not Null)
Insert Into @tblParams Values('CompanyName',@CompanyName)

If(@Description is not Null)
Insert Into @tblParams Values('Description',@Description)

If(@UpdatedBy is not Null)
Insert Into @tblParams Values('UpdatedBy',CONVERT(varchar,@UpdatedBy))

If(@IsActive is not Null)
Insert Into @tblParams Values('IsActive',@IsActive)

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

Set @SQL=@Sql+' WHERE CompanyId='+CONVERT(varchar,@CompanyId) 
EXECute(@sql)