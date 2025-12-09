using JobManagerAPI_v4.Models;

namespace AssetManager.Store.InMemory.Accounts
{
    public interface IAccountsRepository_InMemory
    {
        int CreateAccount(Account account);
        List<Account> GetAllAccounts(int accountOwner);
        Account GetAccountById(int accountId);
        void SetAccountBalance(int accountId, int accountBalance);
        void UpdateAccount(Account account);
    }
}
