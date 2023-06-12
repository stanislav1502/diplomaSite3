using DiplomaSite3.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace DiplomaSite3.Data
{
    public class MyPassHashing : IPasswordHasher<UserModel>
    {
        public string HashPassword(UserModel user, string password)
        {
            var salt = user.PasswordSalt;
            if (salt == null)
            {

                Random rng = new Random();
                byte[] newsalt = new byte[32];
                rng.NextBytes(newsalt);
                user.PasswordSalt = newsalt;
                
                salt = user.PasswordSalt;
            }
            var hash = Rfc2898DeriveBytes.Pbkdf2(password,salt,35716,HashAlgorithmName.SHA512,100);
            return hash.ToString();
        }

        public PasswordVerificationResult VerifyHashedPassword(UserModel user, string hashedPassword, string providedPassword)
        {
            if (user != null) 
            {
                if (HashPassword(user,providedPassword).Equals(hashedPassword))
                    return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }
    }
}
