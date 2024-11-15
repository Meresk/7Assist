using System.Text;
using System.Security.Cryptography;

namespace LiveKit.API.Helpers
{
    public class JwtHelper
    {
        public static string ComputeSha256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
