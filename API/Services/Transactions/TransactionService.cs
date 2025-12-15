using AssetManager.Models;
using AssetManager.Repository.SqlServer.Transactions;

namespace AssetManager.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<int> Create(Transaction transaction)
        {
            try
            {
                if (!validateInputs(transaction))
                    throw new Exception("ERROR_InvalidInput");

                

                int accountId = await _transactionRepository.Create(transaction);
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

        public bool validateInputs(Transaction transaction)
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

        public async Task<IEnumerable<Transaction>> GetAll()
        {
            try
            {
                return await _transactionRepository.GetAll();
            } 
            catch
            {
                throw;
            }
        }

        public async Task<Transaction> GetById(int accountId = 0)
        {
            try
            {
                if (accountId == 0)
                    throw new Exception("ERROR_InvalidInput");

                Transaction? transactionById = await _transactionRepository.GetById(accountId);
                if (transactionById == null)
                    throw new Exception("ERROR_InvalidId");

                return transactionById;
            }
            catch
            {
                throw;
            }


        }

        public async Task<bool> Update(Transaction transaction)
        {
            try
            {
                if (validateInputs(transaction))
                    throw new Exception("ERROR_InvalidInputs");

                await _transactionRepository.Update(transaction);
                return true;
            } 
            catch
            {
                throw;
            }
        }

        public async Task<bool> Delete(int transactionId = 0)
        {
            try
            {
                if (transactionId == 0)
                    throw new Exception("ERROR_InvalidInput");

                await _transactionRepository.Delete(transactionId);
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
