using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.ClassLib.Utilities
{
    public class PasswordEncoder
    {
        public static string EncodePassword(string password)
        {
            using var sha = SHA256.Create();
            var computedHash = sha.ComputeHash(Encoding.Unicode.GetBytes(password));
            return Convert.ToBase64String(computedHash);
        }
    }
}
