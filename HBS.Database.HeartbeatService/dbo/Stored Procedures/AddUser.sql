
CREATE PROCEDURE [dbo].[AddUser]

@CompanyId int,
@UserName nvarchar(50),
@Password nvarchar(128),
@PasswordSalt nvarchar(128)=NULL,
@FirstName nvarchar(100)=NULL,
@LastName nvarchar(100)=NULL,
@Email nvarchar(150)=NULL,
@ConfirmationToken nvarchar(128)=NULL,
@IsConfirmed bit=NULL,
@CreatedBy int=NULL,
@UpdatedBy int=NULL



AS


INSERT INTO UserProfile(CompanyId,UserName,Password,PasswordSalt,FirstName,LastName,Email,ConfirmationToken,IsConfirmed,CreatedDate,CreatedBy,UpdatedDate,UpdatedBy) 
VALUES(@companyId,@UserName,@Password,@PasswordSalt,@FirstName,@LastName,@Email,@ConfirmationToken,@IsConfirmed,GetUtcDate(),@CreatedBy,GetUtcDate(),@UpdatedBy)

SELECT @@IDENTITY As UserId