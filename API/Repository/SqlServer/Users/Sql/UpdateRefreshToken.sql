IF NOT EXISTS(SELECT 1 FROM RefreshTokens WHERE tokenHash = @OldTokenHash)
    BEGIN
        ;THROW 50001, 'ERROR_RowNotFound', 1;
    END
ELSE
    BEGIN
        UPDATE RefreshTokens
        SET 
            tokenHash = @NewTokenHash,
            expiresAt = @NewTokenExpiration,
            isRevoked = 0
        WHERE tokenHash = @OldTokenHash
    END

SELECT '1' AS refreshTokenId