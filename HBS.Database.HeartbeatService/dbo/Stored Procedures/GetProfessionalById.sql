
CREATE PROCEDURE [dbo].[GetProfessionalById]
--[GetProfessionalById]  3

@ProfessionalId int


AS

SELECT 
	p.ProfessionalId,
	t.ProfessionalTypeId,
	t.ProfessionalTypeDesc,
	c.CompanyId,
	c.CompanyName,
	p.Title,
	p.FirstName,
	p.MiddleInitial,
	p.LastName,
	p.phone,
	p.ProfessionalIdentificationNumber,
	p.Email,
	p.IsActive
FROM Professional P 
INNER JOIN Company C ON P.CompanyId = C.CompanyId
INNER JOIN ProfessionalType T ON P.ProfessionalTypeId = T.ProfessionalTypeId
WHERE P.ProfessionalId = @ProfessionalId

--GET PROFESSIONAL SCHEDULE BY Professional Id
SELECT * FROM ProfessionalSchedule PS
WHERE PS.ProfessionalId = @ProfessionalId


--GET PROFESSIONAL's APPOINTMENTS AND CUSTOMERS BY Professional Id
SELECT A.*,C.FirstName,c.LastName FROM Appointments A
INNER JOIN Customers C ON A.CustomerId = C.CustomerId
WHERE A.ProfessionalId = @ProfessionalId
