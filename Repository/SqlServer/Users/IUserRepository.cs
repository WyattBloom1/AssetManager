using AssetManager.Models;

namespace AssetManager.Repository.SqlServer.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> CreateUser(User user);

        Task<User?> GetByUserName(string userName);
    }
}
