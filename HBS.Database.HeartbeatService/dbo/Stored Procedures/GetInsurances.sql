
Create PROCEDURE [dbo].[GetInsurances]
-- [GetInsurances]  1, 'z'


@CompanyId int,
@Name nvarchar(50)=Null



AS

SELECT 
	I.InsuranceID,
	c.CompanyId,
	c.CompanyName,
	I.InsuranceName,
	I.InsuranceAddress,
	I.InsurancePhone,
	I.InsuranceWebsite,
	I.IsActive
FROM Insurances I
INNER JOIN Company c
ON I.CompanyId=c.CompanyId
WHERE ((ISNULL(I.InsuranceName,'') like '%'+@Name+'%') or @Name is null)
and c.companyid=@CompanyId