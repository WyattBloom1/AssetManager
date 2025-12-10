INSERT INTO Users(
	UserId,
	UserName,
	PasswordHash,
	Salt,
	Email
)
VALUES (
	@UserId,
	@UserName,
	@PasswordHash,
	@Salt,
	@Email
)
SELECT @@IDENTITY AS userId