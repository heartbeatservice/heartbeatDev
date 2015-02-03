

CREATE PROCEDURE [dbo].GetProfessionalScheduleByDate 
@StartDate date,

@EndDate date=null,

@ProfessionalId int=NULL

AS

declare @sql varchar(256)



if @EndDate is null  

SEt @EndDate=DATEADD(DD,1,@StartDate)


--GetProfessionalScheduleByProfessionalByDate 20140403,null,null   =20140403,@ProfessionalId=1

Declare @tblParams  table(id int identity(1,1),name varchar(50),paramName varchar(50))



SET @Sql='SELECT ProfessionalScheduleId,ProfessionalId,StartTime,EndTime,TimeIntervalMinutes FROM ProfessionalSchedule Where  startTime>'''+CONVERT(varchar,@StartDate,101) + ''' AND StartTime<'''+ CONVERT(varchar,@EndDate,101) +''''











If(@ProfessionalId is not Null)

BEGIN

Insert Into @tblParams Values('ProfessionalId',CONVERT(varchar,@ProfessionalId))

Declare @UpdateColumn varchar(50)







Select @UpdateColumn=name+'='''+paramName+'''' FROM @tblParams WHERE ID=1





Set @SQL=@Sql+' AND '+@UpdateColumn

END

exec (@sql)












