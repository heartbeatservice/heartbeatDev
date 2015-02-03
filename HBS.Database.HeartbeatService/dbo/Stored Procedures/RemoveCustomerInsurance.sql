
Create PROCEDURE [dbo].[RemoveCustomerInsurance]
-- [RemoveCustomerInsurance]  1


@CustomerInsuranceID int



AS

Delete from CustomerInsurance
WHERE CustomerInsuranceID=@CustomerInsuranceID