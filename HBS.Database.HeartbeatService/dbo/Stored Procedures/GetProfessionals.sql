
CREATE PROCEDURE [dbo].[GetProfessionals]
-- [GetProfessionals]  1, 'z'
@CompanyId INT,
@Name NVARCHAR(50)=Null

AS


-- GET PROFESSIONALS DETAILS BY COMPANY
SELECT 
	P.ProfessionalId,
	T.ProfessionalTypeId,
	T.ProfessionalTypeDesc,
	C.CompanyId,
	C.CompanyName,
	P.Title,
	P.FirstName,
	P.MiddleInitial,
	P.LastName,
	P.phone,
	P.ProfessionalIdentificationNumber,
	P.Email,
	P.IsActive
FROM Professional P
INNER JOIN Company C ON P.CompanyId = C.CompanyId
INNER JOIN ProfessionalType T ON P.ProfessionalTypeId = T.ProfessionalTypeId
WHERE ((ISNULL(P.FirstName,'') like '%'+@Name+'%' OR ISNULL(P.LastName,'')  like '%'+@Name+'%' ) or @Name is null)
AND c.companyid = @CompanyId


