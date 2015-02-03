
CREATE PROCEDURE [dbo].[GetUserById] 
--GetUserById  1

@UserId int



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
WHERE u.UserId=@UserId