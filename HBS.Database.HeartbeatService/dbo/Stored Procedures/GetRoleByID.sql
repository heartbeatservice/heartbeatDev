
Create PROCEDURE [dbo].[GetRoleByID]
-- [GetRoleByID]  1


@RoleID int



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
and r.roleid=@RoleID