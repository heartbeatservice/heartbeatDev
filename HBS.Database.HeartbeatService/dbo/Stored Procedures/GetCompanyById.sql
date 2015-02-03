

CREATE PROCEDURE [dbo].[GetCompanyById] 
-- [GetCompanyById] 3

@CompanyId int


AS


Select CompanyId,CompanyName,Description,CreatedDate,CreatedBy,UpdatedDate,Updatedby,IsActive FROM
Company WHERE CompanyId=@CompanyId