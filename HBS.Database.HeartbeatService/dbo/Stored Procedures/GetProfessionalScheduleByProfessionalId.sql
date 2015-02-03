CREATE PROCEDURE dbo.GetProfessionalScheduleByProfessionalId
@ProfessionalId int
AS

SELECT [ProfessionalScheduleId], [ProfessionalId], [StartTime], [EndTime], [TimeIntervalMinutes] FROM ProfessionalSchedule Where ProfessionalId=@ProfessionalId