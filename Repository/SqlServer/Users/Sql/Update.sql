Update Users
SET 
	UserId = isnull(@UserId, UserId),
	UserName = isnull(@UserName, UserName),
	PasswordHash = isnull(@PasswordHash, PasswordHash),
	Salt = isnull(@Salt, Salt),
	Email = isnull(@Email, Email)
WHERE UserId = @UserId