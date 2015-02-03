    
Create PROCEDURE [dbo].[GetCustomers]    
--[GetCustomers]  1    
    
@CompanyId int,  
@Name nvarchar(50)=Null  
    
    
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
WHERE ((ISNULL(C.FirstName,'') like '%'+@Name+'%' OR ISNULL(C.LastName,'')  like '%'+@Name+'%' ) or @Name is null)  
and c.companyid=@CompanyId