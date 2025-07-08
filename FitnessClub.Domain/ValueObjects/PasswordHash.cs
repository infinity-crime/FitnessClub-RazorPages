using FitnessClub.Domain.Exceptions;
using FitnessClub.Domain.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.ValueObjects
{
    public sealed class PasswordHash
    {
        public string Hash { get; }
        public string Salt { get; }

        private PasswordHash(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public static PasswordHash Create(string plainText, IPasswordHasher passwordHasher)
        {
            if (plainText.Length < 8)
                throw new DomainException("Пароль должен содержать 8 или больше символов!");

            var (hash, salt) = passwordHasher.Hash(plainText);

            if (string.IsNullOrWhiteSpace(hash) || string.IsNullOrWhiteSpace(salt))
                throw new DomainException("Password hash or salt cannot be empty!");

            return new PasswordHash(hash, salt);
        }

        public bool Verify(string plainText, IPasswordHasher passwordHasher) =>
            passwordHasher.Verify(plainText, Hash, Salt);
    }
}
