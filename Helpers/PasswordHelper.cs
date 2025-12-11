using AssetManager.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlTypes;
using System.Security.Cryptography;


namespace AssetManager.Helpers.PasswordHelper
{
    public interface IPasswordHelper
    {
        string HashPassword(string password);
        string VerifyPassword(string hashedPassword, string password, string salt);
    }

    public class PasswordHelper : IPasswordHelper //IPasswordHasher<User>
    {

        public string HashPassword(string password)
        {
            const int keySize = 256 / 8;
            var salt = RandomNumberGenerator.GetBytes(keySize);


            Console.WriteLine("Salt: " + salt);
            string encryptedPassword = this.EncryptPassword(password, salt);

            string saltString = Convert.ToBase64String(salt);
            return encryptedPassword + " | " + saltString;
        }

        private string EncryptPassword(string password, byte[] salt)
        {
            const int keySize = 256 / 8;
            const int iterations = 600000;
            var key = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, keySize);
            var base64 = Convert.ToBase64String(key);

            return base64;
        }

        public string VerifyPassword(string hashedPassword, string password, string salt)
        {
            byte[] saltArray = Convert.FromBase64String(salt);
            string newPass = EncryptPassword(password, saltArray);


            string saltString = Convert.ToBase64String(saltArray);
            bool isValid = newPass == hashedPassword;

            //if (newPass == hashedPassword)
                return isValid + " | " + newPass + " | " + hashedPassword;
            //else
                //return false;
        }
    }
}