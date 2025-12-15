using AssetManager.Models;
using AssetManager.Repository.SqlServer.Accounts;

namespace AssetManager.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<int> CreateAccount(Account account)
        {
            try
            {
                if (!validateInputs(account))
                    throw new Exception("ERROR_InvalidInput");

                int accountId = await _accountRepository.Create(account);
                if (accountId == 0)
                    throw new Exception("ERROR_CreateAccountFailed");

                return accountId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool validateInputs(Account account)
        {
            try
            {
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Account> GetAccountById(int accountId = 0)
        {
            try
            {
                if (accountId == 0)
                    throw new Exception("ERROR_InvalidInput");

                Account? accountById = await _accountRepository.GetById(accountId);
                if (accountById == null)
                    throw new Exception("ERROR_InvalidAccountId");

                return accountById;
            }
            catch
            {
                throw;
            }


        }

        public async Task<IEnumerable<AccountHistory>> GetAccountHistory(int accountId)
        {
            try
            {
                if (accountId == 0)
                    throw new Exception("ERROR_InvalidInput");

                IEnumerable<AccountHistory> accountHistory = await _accountRepository.GetAccountHistory(accountId);
                if (accountHistory == null)
                    throw new Exception("ERROR_InvalidAccountId");

                return accountHistory;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            try
            {
                return await _accountRepository.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> SetAccountBalance(int accountId, decimal accountBalance)
        {
            try
            {
                await _accountRepository.UpdateAccountBalance(accountId, accountBalance);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateAccount(Account account)
        {
            try
            {
                if (validateInputs(account))
                    throw new Exception("ERROR_InvalidInputs");

                await _accountRepository.Update(account);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DeleteAccount(int accountId = 0)
        {
            try
            {
                if (accountId == 0)
                    throw new Exception("ERROR_InvalidInput");

                await _accountRepository.Delete(accountId);
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
