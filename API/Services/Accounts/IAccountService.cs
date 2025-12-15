using AssetManager.Models;

namespace AssetManager.Services.Accounts
{
    public interface IAccountService
    {
        Task<int> CreateAccount(Account account);

        Task<IEnumerable<Account>> GetAllAccounts();

        Task<Account> GetAccountById(int accountId);

        Task<IEnumerable<AccountHistory>> GetAccountHistory(int accountId);

        Task<bool> SetAccountBalance(int accountId, decimal accountBalance);

        Task<bool> UpdateAccount(Account account);

        Task<bool> DeleteAccount(int accountId);
    }
}
