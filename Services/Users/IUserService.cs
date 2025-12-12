using AssetManager.Models;

namespace AssetManager.Services.Users
{
    public interface IUserService : IGenericService<User>
    {
        public Task<User> Create(string userName, string password)
        {
            throw new NotImplementedException();
        }

        Task<User> GetByUserName(UserLogin user);

        Task<int> CreateRefreshToken(RefreshTokens refreshToken);

        Task<int> UpdateRefreshToken(RefreshTokens newToken, string oldTokenHash);

        Task<RefreshTokens> VerifyRefreshToken(int userId, string hashedToken);
    }
}
