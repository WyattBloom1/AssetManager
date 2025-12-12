IF NOT EXISTS(SELECT 1 FROM Users WHERE UserId = @UserId)
    BEGIN
        ;THROW 50001, 'ERROR_RowNotFound', 1;
    END
ELSE
    BEGIN
        IF EXISTS(SELECT 1 FROM RefreshTokens WHERE tokenHash = @TokenHash)
			BEGIN
                UPDATE RefreshTokens
                SET 
                    tokenHash = @TokenHash,
                    expiresAt = @ExpiresAt,
                    isRevoked = 0
                WHERE tokenHash = @TokenHash
			END
        ELSE
            BEGIN
                INSERT INTO RefreshTokens(
                    userId,
                    tokenHash,
                    tokenPrefix,
                    expiresAt,
                    isRevoked
                )
                VALUES (
                    @UserId,
                    @TokenHash,
                    @TokenPrefix,
                    @ExpiresAt,
                    @IsRevoked
                )

                SELECT @@IDENTITY AS refreshTokenId
            END
    END