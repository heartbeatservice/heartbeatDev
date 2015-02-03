CREATE PROCEDURE dbo.AddProfessionalSchedule
@ProfessionalId int,
@StartTime DateTime,
@EndTime DateTime,
@TimeIntervalMinutes smallint
AS

INSERT INTO ProfessionalSchedule(ProfessionalId,StartTime,EndTime,TimeIntervalMinutes)
VALUES(@ProfessionalId,@StartTime,@EndTime,@TimeIntervalMinutes)

SELECT @@Identity