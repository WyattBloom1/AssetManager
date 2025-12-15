using AssetManager.Models;
using AssetManager.Repository.SqlServer.AccountTypes;

namespace AssetManager.Services.AccountTypes
{
    public class AccountTypeService : IAccountTypeService
    {
        private readonly IAccountTypeRepository _repository;

        public AccountTypeService(IAccountTypeRepository accountRepository)
        {
            _repository = accountRepository;
        }

        public bool validateInputs(AccountType accountType)
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

        public async Task<int> Create(AccountType createObj)
        {
            try
            {
                if (!validateInputs(createObj))
                    throw new Exception("ERROR_InvalidInput");

                int accountId = await _repository.Create(createObj);
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

        public async Task<IEnumerable<AccountType>> GetAll()
        {
            try
            {
                return await _repository.GetAll();
            }
            catch
            {
                throw;
            }
        }

        public async Task<AccountType> GetById(int id)
        {
            try
            {
                if (id == 0)
                    throw new Exception("ERROR_InvalidInput");

                AccountType? returnObj = await _repository.GetById(id);
                if (returnObj == null)
                    throw new Exception("ERROR_InvalidId");

                return returnObj;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Update(AccountType updateObj)
        {
            try
            {
                if (!validateInputs(updateObj))
                    throw new Exception("ERROR_InvalidInputs");

                await _repository.Update(updateObj);
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                if (id == 0)
                    throw new Exception("ERROR_InvalidInput");

                await _repository.Delete(id);
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
