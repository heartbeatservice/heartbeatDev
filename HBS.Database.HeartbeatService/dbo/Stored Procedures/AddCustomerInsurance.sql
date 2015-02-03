
Create PROCEDURE [dbo].[AddCustomerInsurance]

@InsuranceID int,
@CustomerId int,
@EffectiveDate date,
@EndDate date=NULL,
@PCPName nvarchar(100)=NULL,
@CustomerInsuranceNumber nvarchar(50)=NULL,
@InsuranceType nvarchar(50)=NULL

AS


INSERT INTO CustomerInsurance(InsuranceID,CustomerId,EffectiveDate,EndDate,PCPName,CustomerInsuranceNumber,InsuranceType) 
VALUES(@InsuranceID,@CustomerId,@EffectiveDate,@EndDate,@PCPName,@CustomerInsuranceNumber,@InsuranceType)