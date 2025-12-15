using AssetManager.Models;

namespace AssetManager.Repository.SqlServer.Transactions
{
    public interface ITransactionRepository : IGenericRepository<Transaction>
    {
        public Task<string> InputValidation(int accountId, int userId, int categoryId);
    }
}
