using AssetManager.Models;
using JobManagerAPI_v4.Models;

namespace AssetManager.Repository.SqlServer.Accounts
{
    public interface IAccountsRepository
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<IEnumerable<Account>> GetMyAccounts(int ownerId);
        Task<Account?> GetAccountById(int accountId);
        Task<IEnumerable<AccountHistory>> GetAccountHistory(int accountId);
        Task<int> CreateAccount(Account account);
        Task<bool> UpdateAccountBalance(int accountId, float newBalance);
        Task<bool> UpdateAccount(int accountId, Account account);
        Task<bool> DeleteAccount(int accountId);
    }
}
