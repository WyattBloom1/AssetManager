IF NOT EXISTS(SELECT 1 FROM Users WHERE UserId = @UserId)
    BEGIN
        ;THROW 50001, 'ERROR_RowNotFound', 1;
    END
ELSE
    BEGIN
        INSERT INTO Users(
            UserName,
            UserEmail,
            PasswordHash,
            Salt
        )
        OUTPUT inserted.UserId, inserted.UserName, inserted.UserEmail, inserted.PasswordHash, inserted.Salt
        VALUES (
            @UserName,
            @UserEmail,
            @PasswordHash,
            @Salt
        )
    END