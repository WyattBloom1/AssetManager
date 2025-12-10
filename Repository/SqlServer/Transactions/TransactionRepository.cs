using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using AssetManager.Models;
using AssetManager.SqlServer;

namespace AssetManager.Repository.SqlServer.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly string connectionString;
        private readonly SqlHelper<TransactionRepository> sqlHelper;

        public TransactionRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            if (connectionString == null)
            {
                throw new InvalidOperationException("Missing [SQLServer] connection string.");
            }

            this.connectionString = connectionString;
            sqlHelper = new SqlHelper<TransactionRepository>();
        }

        public async Task<int> Create(Transaction createObj)
        {
            await using var connection = new SqlConnection(connectionString);
            {
                try
                {
                    return await connection.QuerySingleAsync<int>(
                        sqlHelper.GetSqlFromEmbeddedResource("Create"),
                        createObj.toInputParams(),
                        commandType: CommandType.Text);
                }
                catch (SqlException ex)
                {
                    switch(ex.Number)
                    {
                        case 2627:
                            // TODO: Implement custom exception type                            
                            // DuplicateKeyException();
                            //break;
                        case 547:
                            // TODO: Implement custom exception for foreign key violation
                            throw new Exception(ForeignKeyCheck(ex.Message));
                        default:
                            throw new Exception("ERROR_CreateFailed");
                    }
                    throw;
                } catch
                {
                    throw;
                }
            }
        }

        // TODO: Remove switch statement and implement a JSON mapping string instead, or switch to pre-validation instead of post.
        private string ForeignKeyCheck(string errorMessage)
        {
            switch(errorMessage.ToUpper())
            {
                case string s when s.Contains("ACCOUNT"):
                    return "ERROR_InvalidAccountID";
                case string s when s.Contains("USER"):
                    return "ERROR_InvalidUserID";
                case string s when s.Contains("CATEGOR"):
                    return "ERROR_InvalidCategoryID";
                default:
                    return "ERROR_CreateFailed";
            }
        }

        public async Task<string> InputValidation(int accountId, int userId, int categoryId)
        {
            await using var connection = new SqlConnection(connectionString);
            return await connection.QuerySingleAsync(
                sqlHelper.GetSqlFromEmbeddedResource("TransactionValidation"),
                commandType: CommandType.Text
            );
        }


        public async Task<IEnumerable<Transaction>> GetAll()
        {
            await using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Transaction>(
                sqlHelper.GetSqlFromEmbeddedResource("GetAll"),
                commandType: CommandType.Text
            );
        }

        public async Task<Transaction?> GetById(int id)
        {
            await using var connection = new SqlConnection(this.connectionString);
            return await connection.QuerySingleAsync<Transaction>(
                sqlHelper.GetSqlFromEmbeddedResource("GetById"),
                new { id },
                commandType: CommandType.Text
            );
        }

        public async Task<bool> Update(Transaction updateObj)
        {
            await using var connection = new SqlConnection(this.connectionString);
            await connection.ExecuteAsync(
                sqlHelper.GetSqlFromEmbeddedResource("Update"),
                updateObj.toInputParams(),
                commandType: CommandType.Text
            );
            return true;
        }

        public async Task<bool> Delete(int accountTypeId)
        {
            await using var connection = new SqlConnection(this.connectionString);
            await connection.ExecuteAsync(
                sqlHelper.GetSqlFromEmbeddedResource("Delete"),
                new { accountTypeId },
                commandType: CommandType.Text
            );
            return true;
        }
    }
}
