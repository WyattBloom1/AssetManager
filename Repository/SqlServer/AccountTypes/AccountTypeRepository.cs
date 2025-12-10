using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using AssetManager.Models;
using AssetManager.SqlServer;

namespace AssetManager.Repository.SqlServer.AccountTypes
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly string connectionString;
        private readonly SqlHelper<AccountTypeRepository> sqlHelper;


        public AccountTypeRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            if (connectionString == null)
            {
                throw new InvalidOperationException("Missing [SQLServer] connection string.");
            }

            this.connectionString = connectionString;
            sqlHelper = new SqlHelper<AccountTypeRepository>();
        }


        public async Task<int> Create(AccountType accountType)
        {
            await using var connection = new SqlConnection(connectionString);
            {
                var parameters = new
                {
                    accountType.TypeName,
                    accountType.TypeDescription,
                    accountType.IsDebt,
                };

                try
                {
                    return await connection.QuerySingleAsync<int>(
                        sqlHelper.GetSqlFromEmbeddedResource("Create"),
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

        public async Task<IEnumerable<AccountType>> GetAll()
        {
            await using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<AccountType>(
                sqlHelper.GetSqlFromEmbeddedResource("GetAll"),
                commandType: CommandType.Text
            );
        }
        public async Task<AccountType?> GetById(int accountTypeId)
        {
            await using var connection = new SqlConnection(this.connectionString);
            return await connection.QuerySingleAsync<AccountType>(
                sqlHelper.GetSqlFromEmbeddedResource("GetId"),
                new { TypeId = accountTypeId },
                commandType: CommandType.Text
            );
        }

        public async Task<bool> Update(AccountType updateObj)
        {
            try
            {
                await using var connection = new SqlConnection(this.connectionString);
                await connection.ExecuteAsync(
                    sqlHelper.GetSqlFromEmbeddedResource("Update"),
                    updateObj.toInputParams(),
                    commandType: CommandType.Text
                );
                return true;
            } catch
            {
                throw;
            }

        }

        public async Task<bool> Delete(int accountTypeId)
        {
            await using var connection = new SqlConnection(this.connectionString);
            await connection.ExecuteAsync(
                sqlHelper.GetSqlFromEmbeddedResource("Delete"),
                new { TypeId = accountTypeId },
                commandType: CommandType.Text
            );
            return true;
        }
    }
}
