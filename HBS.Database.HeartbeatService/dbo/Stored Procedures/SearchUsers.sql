
CREATE PROCEDURE [dbo].[SearchUsers]
--[SearchUsers]  1, 'naveed'
@CompanyId int,
@SearchText nvarchar(100)




AS

SELECT 
    u.UserId,
	c.CompanyId,
	c.CompanyName,
	u.UserName,
	u.Password,
	u.PasswordSalt,
	u.FirstName,
	u.LastName,
	u.Email,
	u.ConfirmationToken,
	u.IsConfirmed,
	u.CreatedDate,
	u.CreatedBy,
	u.UpdatedDate,
	u.UpdatedBy,
	u.IsActive
FROM UserProfile u
INNER JOIN Company c
ON u.CompanyId=c.CompanyId
WHERE (ISNULL(u.FirstName,'') like '%'+@SearchText+'%' OR ISNULL(u.LastName,'')  like '%'+@SearchText+'%' )