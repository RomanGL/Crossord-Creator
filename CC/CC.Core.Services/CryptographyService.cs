using CC.Core.Services.Interfaces;
using System.Text;

namespace CC.Core.Services
{
    public sealed class CryptographyService : ICryptographyService
    {
        public string GetSHA1(string input)
        {
            byte[] hash;
            using (var sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider())
                hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sb = new StringBuilder();

            foreach (byte b in hash)
                sb.AppendFormat("{0:x2}", b);

            return sb.ToString();
        }
    }
}
