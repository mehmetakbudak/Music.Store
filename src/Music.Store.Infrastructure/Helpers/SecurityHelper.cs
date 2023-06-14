using System;
using System.Security.Cryptography;
using System.Text;

namespace Music.Store.Infrastructure.Helpers
{
    public static class SecurityHelper
    {
        public static string Sha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string MD5Crypt(string text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] btr = Encoding.UTF8.GetBytes(text);
                btr = md5.ComputeHash(btr);
                StringBuilder sb = new StringBuilder();
                foreach (byte ba in btr)
                {
                    sb.Append(ba.ToString("x2").ToLower());
                }
                return sb.ToString();
            }
        }

        public static string RandomBase64(int bitCount = 64)
        {
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var buffer = new byte[bitCount];
                randomNumberGenerator.GetBytes(buffer);
                var randomKey = Convert.ToBase64String(buffer);
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(randomKey));
            }
        }
    }
}
