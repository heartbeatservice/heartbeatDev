
CREATE Procedure [dbo].[AddAppointment]
@ProfessionalId int,
@CustomerId int,
@AppointmentDate date,
@AppointmentStartTime nvarchar(50),
@StatusId int=NULL,
@Comments nvarchar(1000),
@CreatedBy int=NULL,
@DateCreated datetime=NULL,
@UpdatedBy int=NULL,
@DateUpdated DateTime=NULL
AS
--AddAppointment 1, 2,GetDate(), '8:00 AM',1,'Hello'
If @DateCreated is null
SET @DateCreated=getUTCDate()

If @DateUpdated is null
SET @DateUpdated=getUTCDate()


INSERT INTO Appointments(ProfessionalId,CustomerId,AppointmentDate,AppointmentStartTime,StatusId,Comments,CreatedBy,DateCreated,UpdatedBy,DateUpdated)
VALUES(@ProfessionalId,@CustomerId,@AppointmentDate,@AppointmentStartTime,@StatusId,@Comments,@CreatedBy,@DateCreated,@UpdatedBy,@Dateupdated)

SELECT @@Identity