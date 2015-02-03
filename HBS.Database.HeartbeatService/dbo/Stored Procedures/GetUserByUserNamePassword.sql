
CREATE PROCEDURE [dbo].[GetUserByUserNamePassword]
--GetUserByUserNamePassword  'Umais','abctesting'

@UserName nvarchar(50),
@Password nvarchar(128)



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
WHERE u.UserName =@UserName and Password  COLLATE  Latin1_General_CS_AS =@Password