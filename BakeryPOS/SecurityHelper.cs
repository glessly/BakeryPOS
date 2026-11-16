using System;
using System.Security.Cryptography;

namespace BakeryPOS
{
    public static class SecurityHelper
    {
        public static (string Hash, string Salt) HashPassword(string plainPassword)
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            using (var pbkdf2 = new Rfc2898DeriveBytes(plainPassword, saltBytes, 10000))
            {
                string hash = Convert.ToBase64String(pbkdf2.GetBytes(32));
                return (hash, salt);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            if (string.IsNullOrEmpty(storedHash) || string.IsNullOrEmpty(storedSalt))
                return false;

            byte[] saltBytes = Convert.FromBase64String(storedSalt);
            using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000))
            {
                string computedHash = Convert.ToBase64String(pbkdf2.GetBytes(32));
                return computedHash == storedHash;
            }
        }
    }
}