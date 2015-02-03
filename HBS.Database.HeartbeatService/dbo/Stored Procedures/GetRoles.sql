
Create PROCEDURE [dbo].[GetRoles]
-- [GetRoles]  1, 'z'


@CompanyId int,
@Name nvarchar(50)=Null



AS

SELECT 
	r.RoleId,
	r.roleName,
	C.CompanyId,
	C.CompanyName,
	r.createdby,
	r.createddate,
	r.UpdatedBy,
	r.DateUpdated
FROM Roles r
INNER JOIN Company c
ON r.CompanyId=c.CompanyId
WHERE ((ISNULL(r.RoleName,'') like '%'+@Name+'%') or @Name is null)
and c.companyid=@CompanyId