using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessClub.Domain.Exceptions;

namespace FitnessClub.Domain.ValueObjects
{
    public sealed class Email 
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Email Create(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.Contains('@'))
                throw new DomainException("Invalid email format!");

            return new Email(email);
        }

        public override string ToString() => Value;

        public override bool Equals(object? obj) => 
            obj is Email other && other.Value == this.Value;

        public override int GetHashCode() => Value.GetHashCode();
    }
}
