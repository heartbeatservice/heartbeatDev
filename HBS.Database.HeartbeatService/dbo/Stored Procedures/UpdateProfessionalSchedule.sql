

CREATE PROCEDURE [dbo].[UpdateProfessionalSchedule] 
--UpdateUser 1,1,'ABCTesting','','Umais','Siddiqui','umais@heartbeatservice.com'
@ProfessionalScheduleId int,
@StartTime DateTime=NULL,
@EndTime DateTime=NULL,
@TimeIntervalMinutes smallint=NULL


AS





declare @sql varchar(256)
Declare @tblParams  table(id int identity(1,1),name varchar(50),paramName varchar(50))

SET @Sql='Update  ProfessionalSchedule
Set'

If(@StartTime is not Null)
Insert Into @tblParams Values('StartTime',CONVERT(varchar,@StartTime))



If(@EndTime is not Null)
Insert Into @tblParams Values('EndTime',CONVERT(varchar,@EndTime))

If(@TimeIntervalMinutes is not Null)
Insert Into @tblParams Values('TimeIntervalMinutes',CONVERT(varchar,@TimeIntervalMinutes))




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

Set @SQL=@Sql+' WHERE ProfessionalScheduleId='+CONVERT(varchar,@ProfessionalScheduleId) 
EXECute(@sql)