﻿CREATE PROCEDURE dbo.GetProfessionalScheduleById
@ProfessionalScheduleId int
AS

SELECT ProfessionalId,StartTime,EndTime,TimeIntervalMinutes FROM ProfessionalSchedule Where ProfessionalScheduleId=@ProfessionalScheduleId