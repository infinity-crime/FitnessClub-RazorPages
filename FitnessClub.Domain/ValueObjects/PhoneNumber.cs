using FitnessClub.Domain.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.ValueObjects
{
    public sealed class PhoneNumber
    {
        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public static PhoneNumber Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainException("Номер телефона не может быть пустым");

            if (phone.Length != 12 || !phone.Contains("+7"))
                throw new DomainException("Номер должен содержать 11 цифр и + в начале");

            return new PhoneNumber(phone);
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj) =>
            obj is PhoneNumber other && other.Value == this.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
