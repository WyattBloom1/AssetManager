IF NOT EXISTS(SELECT 1 FROM Users WHERE UserId = @UserId)
    BEGIN
        ;THROW 50001, 'ERROR_RowNotFound', 1;
    END
ELSE
    BEGIN
        SELECT 1
            UserId,
            UserName,
            UserEmail,
            PasswordHash,
            Salt
        FROM Users
        WHERE UserId = @UserId
    END