  
Create PROCEDURE [dbo].[GetCustomerByID]  
--[GetCustomerByID]  3  
  
@CustomerId int  
  
  
AS  
  
SELECT   
 C.CustomerId,  
 P.CompanyID,  
 P.CompanyName,  
 c.FirstName,  
 c.MiddleInitial,
 c.LastName,  
 c.DateOfBirth,  
 c.Address1,
 c.Address2,
 c.City,
 c.State,
 c.Zip,
 c.HomePhone,
 c.CellPhone,
 c.Email,
 c.CreatedBy,
 c.DateCreated,
 c.UpdatedBy,
 c.DateUpdated
FROM Customers C  
inner join Company P  
on C.CompanyID=P.CompanyID
WHERE C.CustomerID=@CustomerId