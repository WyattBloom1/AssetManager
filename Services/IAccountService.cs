using JobManagerAPI_v4.Models;

namespace JobManagerAPI_v4.Services
{
    public interface IAccountService
    {
        int CreateAccount(Account account);

        List<Account> GetAllAccounts(int accountOwner);

        Account GetAccountById(int accountId);

        void SetAccountBalance(int accountId, int accountBalance);

        void UpdateAccount(Account account);
    }
}
