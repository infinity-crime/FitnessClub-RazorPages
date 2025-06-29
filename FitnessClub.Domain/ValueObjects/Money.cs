using FitnessClub.Domain.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.ValueObjects
{
    public sealed class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money Create(decimal amount, string currency)
        {
            if (amount <= 0)
                throw new DomainException("Amount must be greater than zero!");

            if (string.IsNullOrWhiteSpace(currency))
                throw new DomainException("Currency must be provided!");

            return new Money(amount, currency);
        }

        public override string ToString() => $"{Amount} {Currency}";

        public override bool Equals(object? obj) =>
            obj is Money other 
            && other.Amount == this.Amount
            && other.Currency == this.Currency;

        public override int GetHashCode() => Amount.GetHashCode() + Currency.GetHashCode();
    }
}
