using DiplomaSite3.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Configuration;
using System.Dynamic;
using System.Reflection;
using System.Security.Cryptography;

namespace DiplomaSite3.Data
{
    public class MyPassHashing : IPasswordHasher<UserModel>
    {
       private void GetSecrets()
        {
            // get local secrets file
            var secretsId = Assembly.GetExecutingAssembly().GetCustomAttribute<UserSecretsIdAttribute>().UserSecretsId;
            var secretsPath = PathHelper.GetSecretsPathFromSecretsId(secretsId);
            // Load 
            var secretsJson = File.ReadAllText(secretsPath);
            secrets = JsonConvert.DeserializeObject<ExpandoObject>(secretsJson, new ExpandoObjectConverter());
        }

        private dynamic secrets;
        private static int iterations = 10000;

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
            if (iterations == 1)
            {
                GetSecrets();
                iterations = int.Parse(secrets.hashIterations);
            } 
            
            var hash = Rfc2898DeriveBytes.Pbkdf2(password,salt,iterations,HashAlgorithmName.SHA512,100);

#pragma warning disable CS8603 // Possible Null reference return.            
            return hash.ToString();
#pragma warning restore CS8603 // Possible Null reference return.

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
