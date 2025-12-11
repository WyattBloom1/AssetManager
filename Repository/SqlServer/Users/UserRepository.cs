using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;
using AssetManager.Models;
using AssetManager.SqlServer;

namespace AssetManager.Repository.SqlServer.Users
{
    public class UserRepository: IUserRepository
    {
        private readonly string connectionString;
        private readonly SqlHelper<UserRepository> sqlHelper;


        public UserRepository(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            if (connectionString == null)
            {
                throw new InvalidOperationException("Missing [SQLServer] connection string.");
            }

            this.connectionString = connectionString;
            sqlHelper = new SqlHelper<UserRepository>();
        }


        public Task<int> Create(User createObj)
        { throw new NotImplementedException(); }

        public async Task<User> CreateUser(User user)
        {
            await using var connection = new SqlConnection(connectionString);
            var parameters = new{ };

            try {
                return await connection.QuerySingleAsync<User>(
                    sqlHelper.GetSqlFromEmbeddedResource("Create"),
                    user.toInputParams(),
                    commandType: CommandType.Text);
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    throw new Exception("ERROR_CreateFailed"); // DuplicateKeyException();

                throw;
            }
        }

        public async Task<User?> GetByUserName(string userName) 
        {
            try
            {
                await using var connection = new SqlConnection(this.connectionString);
                return await connection.QuerySingleAsync<User>(
                    sqlHelper.GetSqlFromEmbeddedResource("GetByUserName"),
                    new { UserName = userName },
                    commandType: CommandType.Text
                );
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2627:
                        // TODO: Implement custom exception type                            
                        // DuplicateKeyException();
                        throw new Exception("ERROR_DuplicateKeyException");
                    case 547:
                        // TODO: Implement custom exception for foreign key violation
                        throw new Exception("ERROR_ForeignKeyViolation");
                    // Row not found
                    case 50001:
                        throw new Exception("ERROR_UserNotFound");
                    default:
                        throw new Exception("ERROR_CreateFailed");
                }
                throw;
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            try {
                await using var connection = new SqlConnection(connectionString);
                return await connection.QueryAsync<User>(
                    sqlHelper.GetSqlFromEmbeddedResource("GetAll"),
                    commandType: CommandType.Text
                );
            }
            catch
            {
                throw;
            }
        }

        public async Task<User?> GetById(int rowId)
        {
            try
            {
                var parameters = new { };

                await using var connection = new SqlConnection(this.connectionString);
                return await connection.QuerySingleAsync<User>(
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

        public async Task<bool> Update(User updateObj)
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
