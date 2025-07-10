using FitnessClub.Domain.Services;
using FitnessClub.Domain.ValueObjects;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public Email Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public FullName FullName { get; private set; }
        public PasswordHash PasswordHash { get; private set; }

        // Навигационное свойство для подписок
        private readonly List<Subscription> _subscriptions = new();
        public IReadOnlyCollection<Subscription> Subscriptions => _subscriptions.AsReadOnly();

        private User() { }

        public User(Guid id, Email email, PhoneNumber phoneNumber, FullName fullname, PasswordHash passwordHash)
        {
            Id = id;
            Email = email;
            PhoneNumber = phoneNumber;
            FullName = fullname;
            PasswordHash = passwordHash;
        }

        public static User Register(string email, string phone, string plainPassword, IPasswordHasher hasher, FullName name)
        {
            var id = Guid.NewGuid();
            var userEmail = Email.Create(email);
            var userPhone = PhoneNumber.Create(phone);
            var passwordHash = PasswordHash.Create(plainPassword, hasher);

            return new User(id, userEmail, userPhone, name, passwordHash);
        }

        public bool VerifyPassword(string password, IPasswordHasher hasher)
        {
            return hasher.Verify(password, PasswordHash.Hash, PasswordHash.Salt);
        }
    }
}
