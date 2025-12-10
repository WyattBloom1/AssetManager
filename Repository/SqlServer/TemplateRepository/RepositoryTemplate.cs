using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using AssetManager.Models;
using AssetManager.SqlServer;

namespace AssetManager.Repository.SqlServer.TemplateRepository
{
    public class TemplateRepository : ITemplateRepository
    {
        private readonly string connectionString;
        private readonly SqlHelper<TemplateRepository> sqlHelper;


        public TemplateRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            if (connectionString == null)
            {
                throw new InvalidOperationException("Missing [SQLServer] connection string.");
            }

            this.connectionString = connectionString;
            sqlHelper = new SqlHelper<TemplateRepository>();
        }

        public async Task<int> Create(Object account)
        {
            await using var connection = new SqlConnection(connectionString);
            var parameters = new{ };

            try {
                return await connection.QuerySingleAsync<int>(
                    sqlHelper.GetSqlFromEmbeddedResource("Create"),
                    parameters,
                    commandType: CommandType.Text);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    throw new Exception("ERROR_CreateFailed"); // DuplicateKeyException();

                throw;
            }
        }

        public async Task<IEnumerable<Object>> GetAll()
        {
            try {
                await using var connection = new SqlConnection(connectionString);
                return await connection.QueryAsync<Object>(
                    sqlHelper.GetSqlFromEmbeddedResource("GetAll"),
                    commandType: CommandType.Text
                );
            }
            catch
            {
                throw;
            }
        }

        public async Task<Object?> GetById(int rowId)
        {
            try
            {
                var parameters = new { };

                await using var connection = new SqlConnection(this.connectionString);
                return await connection.QuerySingleAsync<AccountType>(
                    sqlHelper.GetSqlFromEmbeddedResource("GetId"),
                    parameters,
                    commandType: CommandType.Text
                );
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> Update(Object updateObj)
        {
            try
            {
                var parameters = new { };

                await using var connection = new SqlConnection(this.connectionString);
                await connection.ExecuteAsync(
                    sqlHelper.GetSqlFromEmbeddedResource("Update"),
                    parameters,
                    commandType: CommandType.Text
                );
                return true;
            } 
            catch
            {
                throw;
            }

        }

        public async Task<bool> Delete(int rowId)
        {
            try
            {
                await using var connection = new SqlConnection(this.connectionString);
                await connection.ExecuteAsync(
                    sqlHelper.GetSqlFromEmbeddedResource("Delete"),
                    new { RowId = rowId },
                    commandType: CommandType.Text
                );
                return true;
            } 
            catch
            {
                throw;
            }
        }
    }
}
