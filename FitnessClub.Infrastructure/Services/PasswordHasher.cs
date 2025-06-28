using FitnessClub.Domain.Services;

using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Infrastructure.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public (string hash, string salt) Hash(string plainText)
        {
            var saltBytes = new byte[128 / 8];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            var saltString = Convert.ToBase64String(saltBytes);

            var hashString = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainText,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 1000,
                numBytesRequested: 256 / 8
                ));

            return (hashString, saltString);
        }

        public bool Verify(string plainText, string hash, string salt)
        {
            var saltBytes = Convert.FromBase64String(salt);

            var testHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: plainText,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100,
                numBytesRequested: 256 / 8
                ));

            return testHash == hash;
        }
    }
}
