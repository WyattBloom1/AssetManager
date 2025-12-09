using JobManagerAPI_v4.Models;

namespace JobManagerAPI_v4.Repository
{
    public class AccountRepository : IAccountRepository
    {
        static List<Account> accounts = new List<Account>();
        static int _accountId = 0;
        public int CreateAccount(Account account)
        {
            _accountId = _accountId++;

            account.AccountId = ++_accountId;
            accounts.Add(account);


            return account.AccountId;
        }

        public Account GetAccountById(int accountId)
        {
            return accounts.First(account => account.AccountId == accountId);
        }

        public List<Account> GetAllAccounts(int accountOwner)
        {
            if(accountOwner == 0)
                return accounts;
            else
                return accounts.FindAll(account => account.AccountOwner == accountOwner);
        }

        public void SetAccountBalance(int accountId, int accountBalance)
        {
            Account accountToUpdate = accounts.First(account => account.AccountId == accountId);

            if (accountToUpdate != null)
            {
                accountToUpdate.AccountBalance = accountBalance;

            }
        }
         
        public void UpdateAccount(Account account)
        {
            Account accountToUpdate = accounts.First(_account => _account.AccountId == account.AccountId);
            if (accountToUpdate != null)
            {
                accountToUpdate = account;
            }
        }
    }
}
