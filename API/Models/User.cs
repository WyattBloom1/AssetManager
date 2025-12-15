using System.Text.Json.Serialization;

namespace AssetManager.Models
{
    //{
    //    get
    //    {
    //        return this.Password;
    //    }
    //    set
    //    {
    //        string hashedPassword = value;
    //        this.PasswordHash = hashedPassword;
    //    }
    //}

    public class UserLogin
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public bool validate()
        {
            if (string.IsNullOrEmpty(this.UserName) || string.IsNullOrEmpty(this.Password))
                return false;

            return true;
        }
    }

    public abstract class BaseUser
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class User : BaseUser
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;

        //System.Int32 UserId, System.String UserName, System.String PasswordHash, System.String Salt, System.String UserEmail
        public User(int userId, string userName, string userEmail, string passwordHash, string salt)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.UserEmail = userEmail;
            this.PasswordHash = passwordHash;
            this.Salt = salt;
        }

        public User(string userName, string passwordHash, string salt)
        {
            this.UserName = userName;
            this.PasswordHash = passwordHash;
            this.Salt = salt;
        }

        public Object toInputParams()
        {
            return new
            {
                UserId = this.UserId,
                UserName = this.UserName,
                PasswordHash = this.PasswordHash,
                Salt = this.Salt,
                UserEmail = this.UserEmail
            };
        }
    }
}
