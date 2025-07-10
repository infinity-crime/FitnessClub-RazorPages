using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FitnessClub.Domain.Entities.Subscription;

namespace FitnessClub.Application.DTOs
{
    public class SubscriptionDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MembershipPlanId { get; set; }
        public string? MembershipPlanName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
