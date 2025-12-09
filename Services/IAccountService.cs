using AssetManager.Models;
using JobManagerAPI_v4.Models;

namespace JobManagerAPI_v4.Services
{
    public interface IAccountService
    {
        Task<int> CreateAccount(Account account);

        Task<IEnumerable<Account>> GetAllAccounts();

        Task<Account> GetAccountById(int accountId);

        Task<IEnumerable<AccountHistory>> GetAccountHistory(int accountId);

        Task<bool> SetAccountBalance(int accountId, float accountBalance);

        Task<bool> UpdateAccount(Account account);

        Task<bool> DeleteAccount(int accountId);
    }
}
