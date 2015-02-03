
CREATE PROCEDURE [dbo].[IsUsernameexists] 
-- IsUsernameexists  'Umais'

@UserName nvarchar(50)


AS

Declare @IsUserExists bit

If Exists  (
SELECT 'x' 				
FROM UserProfile u
WHERE u.UserName=@UserName)

Set @IsUserExists=1 else
Set @IsUserExists=0

Select @IsUserExists