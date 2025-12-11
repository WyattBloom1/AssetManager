IF EXISTS(SELECT 1 FROM Users WHERE UserName = @UserName)

    BEGIN
        SELECT 
            UserId,
            UserName,
            UserEmail,
            PasswordHash,
            Salt
        FROM Users
        WHERE UserName = @UserName
    END

ELSE
    BEGIN

        ;THROW 50001, 'ERROR_RowNotFound', 1;

    END