using System.Text.Json.Serialization;

namespace AssetManager.Models
{
    public abstract class BaseUser
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class User : BaseUser
    {
        public int UserId { get; set; }

        [JsonIgnore]
        public string PasswordHash {
            get
            {
                return this.Password;
            }
            set
            {
                string hashedPassword = value;
                this.PasswordHash = hashedPassword;
            }
        }
        public string Salt { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public User(int userId, string userName, string passwordHash, string salt, string email)
        {
            this.UserId = userId;
            this.UserName = userName;
            this.PasswordHash = passwordHash;
            this.Salt = Salt;
            this.Email = email;
        }

        public Object toInputParams()
        {
            return new
            {
                UserId = this.UserId,
                UserName = this.UserName,
                PasswordHash = this.PasswordHash,
                Salt = this.Salt,
                Email = this.Email
            };
        }
    }
}
