using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonClassLibrary
{
    public static class HashHelper
    {
        public static string GenerateSalt(int length = 32)
        {
            // Using RNGCryptoServiceProvider to generate a secure random salt
            var saltBytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string Hash(string password, string salt)
        {
            var combinedPassword = salt + password;

            // Hash the combined password using SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedPassword));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
