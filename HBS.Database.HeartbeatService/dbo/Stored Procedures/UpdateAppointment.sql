

CREATE PROCEDURE [dbo].[UpdateAppointment] 
--UpdateAppointment
@AppointmentId int,
@ProfessionalId int=NULL,
@CustomerId int=NULL,
@AppointmentDate date=NULL,
@AppointmentStartTime nvarchar(50)=NULL,
@StatusId int=NULL,
@Comments nvarchar(1000)=null,
@CreatedBy int=NULL,
@DateCreated datetime=NULL,
@UpdatedBy int=NULL,
@DateUpdated DateTime=NULL

AS
--UpdateAppointment @AppointmentId=1,@statusId=2
DECLARE @UpdatedDate DATETime=GetUtcDate();

declare @sql varchar(256)
Declare @tblParams  table(id int identity(1,1),name varchar(50),paramName varchar(50))

SET @Sql='Update Appointments
Set'

If(@ProfessionalId is not Null)
Insert Into @tblParams Values('ProfessionalId',@ProfessionalId)


If(@CustomerId is not Null)
Insert Into @tblParams Values('CustomerId',@CustomerId)


If(@AppointmentDate is not Null)
Insert Into @tblParams Values('AppointmentDate',CONVERT(varchar,@AppointmentDate))

If(@AppointmentStartTime  is not Null)
Insert Into @tblParams Values('AppointmentStartTime ',@AppointmentStartTime )

If(@StatusId is not Null)
Insert Into @tblParams Values('StatusId',CONVERT(varchar,@StatusId))

If(@Comments is not Null)
Insert Into @tblParams Values('Comments',@Comments)

If(@CreatedBy is not Null)
Insert Into @tblParams Values('CreatedBy',CONVERT(varchar,@CreatedBy))

If(@DateCreated is not Null)
Insert Into @tblParams Values('Phone',CONVERT(varchar,@DateCreated))

If(@UpdatedBy is not Null)
Insert Into @tblParams Values('Updatedby',CONVERT(varchar,@UpdatedBy))

If(@DateUpdated is not Null)
Insert Into @tblParams Values('Email',CONVERT(varchar,@DateUpdated))



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

Set @SQL=@Sql+' WHERE AppointmentID='+CONVERT(varchar,@ProfessionalID) 
EXECute(@sql)