
Create PROCEDURE [dbo].[IsUserInRole] 
-- IsUserInRole  'Umais'

@UserID int,
@RoleID int


AS

Declare @IsUserInRole bit

If Exists  (
SELECT 'x' 				
FROM UserRole u
WHERE u.UserID=@UserID and u.roleid=@RoleID)

Set @IsUserInRole=1 else
Set @IsUserInRole=0

Select @IsUserInRole