
CREATE PROCEDURE [dbo].[GetUserRolesByID]
-- [GetUserRolesByID]  1


@UserID int



AS

SELECT 
	p.UserID,
	p.UserName,
	p.FirstName,
	p.LastName,
	r.RoleId,
	r.roleName,
	C.CompanyId,
	C.CompanyName
FROM UserRole u
inner join UserProfile p
on u.userid=p.userid
INNER JOIN Company c
ON p.CompanyId=c.CompanyId
inner join Roles r
on r.roleid=u.roleid
where u.UserID=@UserID