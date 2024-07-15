using System.Security.Cryptography;
using System.Text;

namespace Entities.Model.AnyClasses
{
    public class PasswordManager
    {
        private const string salt = "20ts24tusalt";

        public static string EncryptPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
