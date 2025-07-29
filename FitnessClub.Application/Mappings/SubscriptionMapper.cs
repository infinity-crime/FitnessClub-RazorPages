using FitnessClub.Application.DTOs;
using FitnessClub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.Mappings
{
    public class SubscriptionMapper
    {
        public static SubscriptionDto MapToDto(Subscription subscription)
        {
            return new SubscriptionDto
            {
                Id = subscription.Id,
                UserId = subscription.UserId,
                MembershipPlanId = subscription.MembershipPlanId,
                MembershipPlanName = subscription.MembershipPlan?.Name ?? string.Empty,
                StartDate = subscription.StartDate,
                EndDate = subscription.EndDate,
                Status = subscription.Status,
                LastModifiedDate = subscription.LastModifiedDate
            };
        }
    }
}
