using AssetManager.Models;

namespace AssetManager.Repository.SqlServer.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> CreateUser(User user);

        Task<User?> GetByUserName(string userName);

        Task<int> CreateRefreshToken(RefreshTokens refreshToken);

        Task<int> UpdateRefreshToken(RefreshTokens newToken, string oldTokenHash);

        Task<RefreshTokens> GetRefreshToken(int userId, string hashedToken);
    }
}
