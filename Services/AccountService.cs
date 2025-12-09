using JobManagerAPI_v4.Models;
using JobManagerAPI_v4.Repository;

namespace JobManagerAPI_v4.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public int CreateAccount(Account account)
        {
            try
            {
                if (!validateInputs(account))
                    throw new Exception("ERROR_InvalidInput");

                int accountId = _accountRepository.CreateAccount(account);
                if (accountId == 0)
                    throw new Exception("ERROR_CreateAccountFailed");

                return accountId;
            }
            catch(Exception ex)
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

        public Account GetAccountById(int accountId)
        {
            try
            {
                if (accountId == 0)
                    throw new Exception("ERROR_InvalidInput");

                Account accountById = _accountRepository.GetAccountById(accountId);
                if (accountById == null)
                    throw new Exception("ERROR_InvalidAccountId");

                return accountById;                
            }
            catch
            {
                throw;
            }


        }

        public List<Account> GetAllAccounts(int accountOwner)
        {
            try
            {
                return _accountRepository.GetAllAccounts(accountOwner);
            } 
            catch
            {
                throw;
            }
        }

        public void SetAccountBalance(int accountId, int accountBalance)
        {
            try
            {
                _accountRepository.SetAccountBalance(accountId, accountBalance);
            } 
            catch
            {
                throw;
            }
        }

        public void UpdateAccount(Account account)
        {
            try
            {
                if (validateInputs(account))
                    throw new Exception("ERROR_InvalidInputs");

                _accountRepository.UpdateAccount(account);
            } 
            catch
            {
                throw;
            }
        }
    }
}
