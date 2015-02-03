
Create PROCEDURE [dbo].[GetCustomerInsuranceByID]
--[GetCustomerInsuranceByID]  3

@CustomerInsuranceID int


AS

SELECT 
	I.CustomerInsuranceID,
	n.InsuranceID,
	n.InsuranceName,
	c.CustomerId,
	c.FirstName,
	c.LastName,
	c.DateOfBirth,
	I.EffectiveDate,
	I.EndDate,
	I.PCPName,
	I.CustomerInsuranceNumber,
	I.InsuranceType
FROM CustomerInsurance i
inner join customers c
on i.customerid=c.customerid
inner join Insurances N
on n.insuranceid=i.insuranceid
WHERE i.CustomerInsuranceID=@CustomerInsuranceID