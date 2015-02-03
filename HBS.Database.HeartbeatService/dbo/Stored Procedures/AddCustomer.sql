  
CREATE PROCEDURE [dbo].[AddCustomer]  
-- AddCustomer 1,'Umais','M','Siddiqui','06-12-1980','41983 usdhasuid i',null,'Queens','NY','11580','7183456578','9174563214','umais20@yahoo.com',1

  
@CompanyId int,  
@FirstName nvarchar(50)=NULL,  
@MiddleInitial nvarchar(1)=NULL,  
@LastName nvarchar(50)=NULL,  
@DateofBirth Date=NULL,
@Address1 nvarchar(100)=NULL,  
@Address2 nvarchar(100)=NULL,  
@City nvarchar(50)=NULL,  
@State nvarchar(2)=NULL,  
@ZIP nvarchar(10)=NULL,  
@HomePhone nvarchar(10)=NULL,  
@CellPhone nvarchar(10)=NULL,  
@Email nvarchar(150)=NULL,  
@CreatedBy int=NULL  
  
AS  
  
  
INSERT INTO Customers(CompanyId,FirstName,MiddleInitial,LastName,DateofBirth,Address1,Address2,City,State,ZIP,HomePhone,CellPhone,Email,CreatedBy,DateCreated)   
VALUES(@CompanyId,@FirstName,@MiddleInitial,@LastName,@DateofBirth,@Address1,@Address2,@City,@State,@ZIP,@HomePhone,@CellPhone,@Email,@CreatedBy,GetUtcDate())