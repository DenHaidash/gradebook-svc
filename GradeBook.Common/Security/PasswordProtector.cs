using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace GradeBook.Common.Security
{
    public static class PasswordProtector
    {
        private static string GetRandomString(int length)
        {
            var random = new Random();
            
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, length)
                .Select(x => pool[random.Next(0, pool.Length)]);
               
            return new string(chars.ToArray());
        }
        
        public static string GenerateSalt(int saltLength = 30)
        {
            return GetRandomString(saltLength);
        }

        public static string SaltString(string salt, string str)
        {
            using (var sha256 = new SHA256Managed())
            {
                return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(salt + str)));
            }
        }
    }
}