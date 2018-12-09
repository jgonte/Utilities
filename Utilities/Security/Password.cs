using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace Utilities.Security
{
    public static class Password
    {
        public static string SaltInBase64()
        {
            var salt = new byte[32]; // 256 bits

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public static string ToHashBase64(string plaintextPassword, string storedPasswordSaltBase64)
        {
            var salt = Convert.FromBase64String(storedPasswordSaltBase64);

            var bytearray = KeyDerivation.Pbkdf2(plaintextPassword, salt, KeyDerivationPrf.HMACSHA512, 50000, 24);

            return Convert.ToBase64String(bytearray);
        }

        public static bool Validate(string storedPasswordHashBase64, string storedPasswordSaltBase64, string plaintextToValidate)
        {
            return storedPasswordHashBase64.Equals(ToHashBase64(plaintextToValidate, storedPasswordSaltBase64));
        }
    }
}
