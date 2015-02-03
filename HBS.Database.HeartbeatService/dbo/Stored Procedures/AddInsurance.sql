
Create PROCEDURE [dbo].[AddInsurance] 
(
@CompanyId int,

@InsuranceName nvarchar(50)=NULL,

@InsuranceAddress nvarchar(150)=NULL,

@InsurancePhone nvarchar(15)=NULL,

@InsuranceWebsite nvarchar(250)=NULL
)




AS





INSERT INTO Insurances(CompanyId,InsuranceName,InsuranceAddress,InsurancePhone,InsuranceWebsite) 

VALUES(@CompanyId,@InsuranceName,@InsuranceAddress,@InsurancePhone,@InsuranceWebsite)









