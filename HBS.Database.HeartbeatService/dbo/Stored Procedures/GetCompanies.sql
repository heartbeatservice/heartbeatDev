

CREATE PROCEDURE [dbo].[GetCompanies] 
--[GetCompanies]

@Name nvarchar(50)=Null



AS

Select CompanyId,CompanyName,Description,CreatedDate,CreatedBy,UpdatedDate,Updatedby,IsActive FROM
Company 
WHERE ((ISNULL(CompanyName,'') like '%'+@Name+'%') or @Name is null)