using AssetManager.Models;

namespace AssetManager.Repository.SqlServer.Accounts
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<IEnumerable<Account>> GetMyAccounts(int ownerId);
        Task<IEnumerable<AccountHistory>> GetAccountHistory(int accountId);
        Task<bool> UpdateAccountBalance(int accountId, decimal newBalance);
    }
}
