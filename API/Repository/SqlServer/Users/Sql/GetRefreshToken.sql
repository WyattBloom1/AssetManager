IF EXISTS(SELECT 1 FROM RefreshTokens WHERE tokenHash = @HashedToken)

    BEGIN
        SELECT 
            rowId,
            userId,
            tokenHash,
            tokenPrefix,
            expiresAt,
            isRevoked
        FROM RefreshTokens
        WHERE userId = @UserId AND tokenHash = @HashedToken
    END

ELSE
    BEGIN

        ;THROW 50001, 'ERROR_RowNotFound', 1;

    END