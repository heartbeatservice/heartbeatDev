
Create PROCEDURE [dbo].[GetInsuranceById]
--[GetInsuranceById]  3

@InsuranceID int


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
WHERE I.InsuranceID=@InsuranceID