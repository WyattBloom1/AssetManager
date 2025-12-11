using AssetManager.Helpers.PasswordHelper;
using AssetManager.Models;
using AssetManager.Repository.SqlServer.AccountTypes;
using AssetManager.Repository.SqlServer.Users;

namespace AssetManager.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHelper _passwordHelper;

        public UserService(IUserRepository userRepository, IPasswordHelper passwordHelper)
        {
            _repository = userRepository;
            _passwordHelper = passwordHelper;
        }

        public bool validateInputs(User accountType)
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

        // TODO: Look further into inheritance and how to allow different input types on inherited services
        public Task<int> Create(User createObj) { throw new NotImplementedException(); }

        public async Task<User> Create(string userName, string password)
        {
            try
            {
                User newUser = _passwordHelper.GetUserObjectWithHashedPassword(userName, password);

                User createdUser = await _repository.CreateUser(newUser);
                //if (accountId == 0)
                    //throw new Exception("ERROR_CreateAccountFailed");

                return createdUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<User> GetByUserName(UserLogin user)
        {
            try
            {
                if (user is null || user.validate() == false)
                    throw new Exception("ERROR_InvalidInputs");

                User? sqlUser = await _repository.GetByUserName(user.UserName);
                if (sqlUser == null)
                    throw new Exception("ERROR_InvalidUserName");
    //            return false;
            
                bool isValidPassword = _passwordHelper.VerifyPassword(user.Password, sqlUser.PasswordHash, sqlUser.Salt);

                if(isValidPassword)
                    return sqlUser;
                else
                    throw new Exception("ERROR_InvalidPassword");
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetAll()
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

        public async Task<User> GetById(int id)
        {
            try
            {
                if (id == 0)
                    throw new Exception("ERROR_InvalidInput");

                User? returnObj = await _repository.GetById(id);
                if (returnObj == null)
                    throw new Exception("ERROR_InvalidId");

                return returnObj;
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
