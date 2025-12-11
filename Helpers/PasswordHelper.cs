using AssetManager.Models;
using System.Security.Cryptography;


namespace AssetManager.Helpers.PasswordHelper
{
    public interface IPasswordHelper
    {
        string GetSalt();
        string HashPassword(string password, string salt);
        bool VerifyPassword(string hashedPassword, string password, string salt);
        User GetUserObjectWithHashedPassword(string userName, string password);
    }

    public class PasswordHelper : IPasswordHelper
    {
        // Key size in bytes
        const int keySize = 256 / 8;
        // Number of iterations for the PBKDF2 algorithm
        const int iterations = 600000;

        /// <summary>
        /// Get a random cryptographic salt value to use in the password hashing process
        /// </summary>
        /// <returns>string</returns>
        public string GetSalt()
        {
            var salt = RandomNumberGenerator.GetBytes(keySize);

            string saltString = Convert.ToBase64String(salt);
            return saltString;
        }

        /// <summary>
        /// Hash a password with a given salt using PBKDF2 algorithm
        /// </summary>
        /// <returns>string</returns>
        public string HashPassword(string password, string salt)
        {
            // Convert the salt string to a Byte array
            byte[] saltArray = Convert.FromBase64String(salt);

            // Encrypt the password with the salt and return the encrypted password as a base64 string
            var key = Rfc2898DeriveBytes.Pbkdf2(password, saltArray, iterations, HashAlgorithmName.SHA256, keySize);
            var encryptedPassword = Convert.ToBase64String(key);

            return encryptedPassword;
        }

        /// <summary>
        /// Get a User object with hashed password and salt
        /// </summary>
        /// <returns>User</returns>
        public User GetUserObjectWithHashedPassword(string userName, string password)
        {
            string salt = this.GetSalt();
            string hashedPassword = this.HashPassword(password, salt);

            User user = new User(userName, hashedPassword, salt);
            return user;
        }

        /// <summary>
        /// Verify a password against a hashed password and salt
        /// </summary>
        /// <returns>bool</returns>
        public bool VerifyPassword(string password, string hashedPassword, string salt)
        {
            string newPass = HashPassword(password, salt);

            if (newPass == hashedPassword)
                return true;
            else
                return false;
        }
    }
}