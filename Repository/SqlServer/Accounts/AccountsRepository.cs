using JobManagerAPI_v4.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using AssetManager.Models;

namespace AssetManager.Repository.SqlServer.Accounts
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly string connectionString;
        private readonly SqlHelper<AccountsRepository> sqlHelper;


        public AccountsRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            if (connectionString == null)
            {
                throw new InvalidOperationException("Missing [SQLServer] connection string.");
            }

            this.connectionString = connectionString;
            sqlHelper = new SqlHelper<AccountsRepository>();
        }


        public async Task<int> CreateAccount(Account account)
        {
            await using var connection = new SqlConnection(connectionString);
            {
                var parameters = new
                {
                    account.AccountName,
                    account.AccountType,
                    account.IsDebt,
                    account.IncludeInTotal,
                    account.AccountOwner,
                    account.AccountBalance
                };

                try
                {
                    return await connection.QuerySingleAsync<int>(
                        sqlHelper.GetSqlFromEmbeddedResource("CreateAccount"),
                        parameters,
                        commandType: CommandType.Text);
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        throw new Exception("ERROR_CreateFailed"); // DuplicateKeyException();
                    }

                    throw;
                }
            }
        }

        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            int ownerId = 0;

            await using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Account>(
                sqlHelper.GetSqlFromEmbeddedResource("GetAllAccounts"),
                new { ownerId },
                commandType: CommandType.Text
            );
        }

        public async Task<IEnumerable<Account>> GetMyAccounts(int ownerId)
        {
            await using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<Account>(
                sqlHelper.GetSqlFromEmbeddedResource("GetAllAccounts"),
                new { ownerId },
                commandType: CommandType.Text
            );
        }

        public async Task<bool> DeleteAccount(int accountId)
        {
            await using var connection = new SqlConnection(this.connectionString);
            await connection.ExecuteAsync(
                sqlHelper.GetSqlFromEmbeddedResource("DeleteAccount"),
                new { accountId },
                commandType: CommandType.Text
            );
            return true;
        }

        public async Task<Account?> GetAccountById(int accountId)
        {
            await using var connection = new SqlConnection(this.connectionString);
            return await connection.QuerySingleAsync<Account>(
                sqlHelper.GetSqlFromEmbeddedResource("GetAccountById"),
                new { accountId },
                commandType: CommandType.Text
            );
        }

        public async Task<IEnumerable<AccountHistory>> GetAccountHistory(int accountId)
        {
            await using var connection = new SqlConnection(this.connectionString);
            return await connection.QueryAsync<AccountHistory>(
                sqlHelper.GetSqlFromEmbeddedResource("GetAccountHistory"),
                new { AccountId = accountId },
                commandType: CommandType.Text
            );
        }


        public Task<bool> UpdateAccount(int accountId, Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAccountBalance(int accountId, float newBalance)
        {
            await using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(
                sqlHelper.GetSqlFromEmbeddedResource("UpdateBalance"),
                new { AccountId = accountId, AccountBalance = newBalance },
                commandType: CommandType.Text
            );
            return true;
        }
    }
}
