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
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Phone number cannot be empty");

            if (value.Length != 12 || !value.Contains("+7"))
                throw new DomainException("Non-standard phone number format");

            Value = value;
        }

        public static PhoneNumber Create(string phone)
        {
            return new PhoneNumber(phone);
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj) =>
            obj is PhoneNumber other && other.Value == this.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
