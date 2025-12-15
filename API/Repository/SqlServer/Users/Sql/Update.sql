Update Users
SET 
	UserId = isnull(@UserId, UserId),
	UserName = isnull(@UserName, UserName),
	UserEmail = isnull(@UserEmail, UserEmail),
	PasswordHash = isnull(@PasswordHash, PasswordHash),
	Salt = isnull(@Salt, Salt)
WHERE UserId = @UserId