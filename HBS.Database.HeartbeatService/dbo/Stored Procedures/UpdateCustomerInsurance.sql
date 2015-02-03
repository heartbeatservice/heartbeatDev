
Create PROCEDURE [dbo].[UpdateCustomerInsurance] 
-- UpdateCustomerInsurance 1,null,'Aetnas',null,'866-801-8245','www.aet-na.com',1


@CustomerInsuranceID int,
@InsuranceID  int=NULL,
@EffectiveDate date=NULL,
@EndDate date=NULL,
@PCPName nvarchar(100)=NULL,
@CustomerInsuranceNumber nvarchar(50)=NULL,
@InsuranceType nvarchar(50)=NULL


AS


declare @sql varchar(256)
Declare @tblParams  table(id int identity(1,1),name varchar(50),paramName varchar(50))

SET @Sql='Update CustomerInsurance
Set'


If(@InsuranceID is not Null)
Insert Into @tblParams Values('InsuranceID',@InsuranceID)

If(@EffectiveDate is not Null)
Insert Into @tblParams Values('EffectiveDate',@EffectiveDate)

If(@EndDate is not Null)
Insert Into @tblParams Values('EndDate',@EndDate)

If(@PCPName is not Null)
Insert Into @tblParams Values('PCPName',@PCPName)

If(@CustomerInsuranceNumber is not Null)
Insert Into @tblParams Values('CustomerInsuranceNumber',@CustomerInsuranceNumber)

If(@InsuranceType is not Null)
Insert Into @tblParams Values('InsuranceType',@InsuranceType)


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

Set @SQL=@Sql+' WHERE CustomerInsuranceID='+CONVERT(varchar,@CustomerInsuranceID) 
EXECute(@sql)