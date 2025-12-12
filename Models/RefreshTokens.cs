using AssetManager.Controllers;

namespace AssetManager.Models
{
    public class RefreshTokens
    {
        public int rowId { get; set; }
        public int userId { get; set; }
        public string tokenHash { get; set; } = string.Empty;
        public string tokenPrefix { get; set; } = string.Empty;
        public DateTime expiresAt { get; set; }
        public bool isRevoked { get; set; } = false;

        public RefreshTokens(int rowId, int userId, string tokenHash, string tokenPrefix, DateTime expiresAt, bool isRevoked) 
        {
            this.rowId = rowId;
            this.userId = userId;
            this.tokenHash = tokenHash;
            this.tokenPrefix = tokenPrefix;
            this.expiresAt = expiresAt;
            this.isRevoked = isRevoked;
        }

        public RefreshTokens(int userId, string tokenHash, string tokenPrefix, DateTime expiresAt, bool isRevoked)
        {
            this.userId = userId;
            this.tokenHash = tokenHash;
            this.tokenPrefix = tokenPrefix;
            this.expiresAt = expiresAt;
            this.isRevoked = isRevoked;
        }

        public RefreshTokens(AuthenticatedResponse response, string tokenHash, string tokenPrefix, bool isRevoked)
        {
            this.userId = response.UserId;
            this.tokenHash = tokenHash;
            this.tokenPrefix = tokenPrefix;
            this.expiresAt = response.AccessTokenExpiration;
            this.isRevoked = false;
        }

        public Object toInputParameters()
        {
            return new
            {
                RowId = this.rowId,
                UserId = this.userId,
                TokenHash = this.tokenHash,
                TokenPrefix = this.tokenPrefix,
                ExpiresAt = this.expiresAt,
                IsRevoked = this.isRevoked
            };
        }
    }
}
