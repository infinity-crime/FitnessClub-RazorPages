using FitnessClub.Domain.Exceptions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid MembershipPlanId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public SubscriptionStatus Status { get; private set; }
        public DateTime? LastModifiedDate { get; private set; }

        // Навигационные свойства для удобства запросов EF Core
        public User? User { get; private set; }
        public MembershipPlan? MembershipPlan { get; private set; }

        private Subscription() { }

        private Subscription(Guid id, Guid userId, Guid membershipPlanId, DateTime startDate, 
            DateTime endDate, SubscriptionStatus status)
        {
            Id = id;
            UserId = userId;
            MembershipPlanId = membershipPlanId;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            LastModifiedDate = DateTime.UtcNow;
        }

        public static Subscription Create(User user, MembershipPlan plan)
        {
            if ((user is null) || (plan is null))
                throw new DomainException("User/Membership plan is required!");

            var id = Guid.NewGuid();
            var startDate = DateTime.Now;
            var endDate = startDate.AddMonths(plan.DurationInMonths);

            return new Subscription(id, user.Id, plan.Id, startDate, endDate, SubscriptionStatus.Active);
        }

        public enum SubscriptionStatus
        {
            Active, // активна 
            Frozen, // заморожена
            Expired, // закончилась
            Canceled // отменена
        }
    }
}
