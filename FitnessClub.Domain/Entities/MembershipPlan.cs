using FitnessClub.Domain.ValueObjects;
using FitnessClub.Domain.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.Entities
{
    public class MembershipPlan
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public Money Price { get; private set; }
        public int DurationInMonths { get; private set; }

        private MembershipPlan(Guid id, string name, string description, Money price, int durationInMonths)
        {
            if (id == Guid.Empty)
                throw new DomainException("The plan ID must be non-zero!");

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
                throw new DomainException("Plan name/description is required!");

            if(durationInMonths <= 0)
                throw new DomainException("Duration must be greater than zero!");

            Id = id;
            Name = name;
            Description = description;
            Price = price;
            DurationInMonths = durationInMonths;
        }

        public static MembershipPlan Create(string name, string description, decimal amountPrice, 
            string currencyPrice, int durationInMonths)
        {
            var id = Guid.NewGuid();
            var price = Money.Create(amountPrice, currencyPrice);

            return new MembershipPlan(id, name, description, price, durationInMonths);
        }
    }
}
