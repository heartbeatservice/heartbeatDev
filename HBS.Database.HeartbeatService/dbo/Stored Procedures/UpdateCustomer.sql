  
CREATE PROCEDURE [dbo].[UpdateCustomer]   
--UpdateCustomer 1,'Navid',null,'Marzi',null,null,null,null,null,null,null,null,null,2

@CustomerID int,  
@FirstName nvarchar(50)=NULL,  
@MiddleInitial nvarchar(1)=NULL,  
@LastName nvarchar(50)=NULL,  
@DateofBirth Date=NULL,
@Address1 nvarchar(100)=NULL,  
@Address2 nvarchar(100)=NULL,  
@City nvarchar(50)=NULL,  
@State nvarchar(2)=NULL,  
@ZIP nvarchar(10)=NULL,  
@HomePhone nvarchar(10)=NULL,  
@CellPhone nvarchar(10)=NULL,  
@Email nvarchar(150)=NULL,  
@UpdatedBy int=NULL   
  
AS  
  
DECLARE @DateUpdated DATETime=GetUtcDate();  
  
declare @sql varchar(256)  
Declare @tblParams  table(id int identity(1,1),name varchar(50),paramName varchar(50))  
  
SET @Sql='Update Customers  
Set'  
  
If(@FirstName is not Null)  
Insert Into @tblParams Values('FirstName',@FirstName)  
  
  
If(@MiddleInitial is not Null)  
Insert Into @tblParams Values('MiddleInitial',@MiddleInitial)  
  
If(@LastName is not Null)  
Insert Into @tblParams Values('LastName',@LastName)  
  
  
If(@DateofBirth is not Null)  
Insert Into @tblParams Values('DateofBirth',@DateofBirth)  
  
If(@Address1 is not Null)  
Insert Into @tblParams Values('Address1',@Address1)  

If(@Address2 is not Null)  
Insert Into @tblParams Values('Address2',@Address2)  
  
If(@City is not Null)  
Insert Into @tblParams Values('City',@City)  

If(@State is not Null)  
Insert Into @tblParams Values('State',@State)  
  
If(@Zip is not Null)  
Insert Into @tblParams Values('Zip',@Zip)  
  
If(@HomePhone is not Null)  
Insert Into @tblParams Values('HomePhone',CONVERT(varchar,@HomePhone))  
  
If(@CellPhone is not Null)  
Insert Into @tblParams Values('CellPhone',CONVERT(varchar,@CellPhone))  
  
If(@Email is not Null)  
Insert Into @tblParams Values('Email',CONVERT(varchar,@Email))  
  
  
If(@UpdatedBy is not Null)  
Insert Into @tblParams Values('UpdatedBy',@UpdatedBy)  
  
If(@DateUpdated is not Null)  
Insert Into @tblParams Values('DateUpdated',CONVERT(varchar,@DateUpdated))  
  
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
  
Set @SQL=@Sql+' WHERE CustomerID='+CONVERT(varchar,@CustomerID)   
EXECute(@sql)