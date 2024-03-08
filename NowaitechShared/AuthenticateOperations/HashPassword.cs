using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace NowaitechShared.AuthenticateOperations
{
    public static class HashPassword
    {
        //private readonly static IConfiguration _configuration;


        //private readonly static string HMAC = "HMACSHA512";

        //private readonly static byte[] HMACByte = Encoding.UTF8.GetBytes(HMAC);

        public static string CreateHash(string pass)
        {
            var HMACByte = GetHMACValue();

            var hmac = new HMACSHA512(HMACByte);

            var valueBytes = Encoding.UTF8.GetBytes(pass);

            var hashValue = hmac.ComputeHash(valueBytes);

            return Convert.ToBase64String(hashValue);
        }

        public static bool VerifyPasswordHash(string password, string passwordHash)
        {
            var computedHash = CreateHash(password);

            Console.WriteLine(passwordHash);
            Console.WriteLine(computedHash);

            return computedHash.Equals(passwordHash);
        }

        private static byte[] GetHMACValue()
        {
            string HMAC;

            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json", false, true).Build();

            IConfigurationProvider provider = config.Providers.ToList().First();

            provider.Load();
            provider.TryGet("HMACSHA512:HMACValue", out HMAC);

            byte[] HMACByte = Encoding.UTF8.GetBytes(HMAC);

            return HMACByte;
        }
    }
}
