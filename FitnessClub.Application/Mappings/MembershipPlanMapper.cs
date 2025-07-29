using FitnessClub.Application.DTOs;
using FitnessClub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.Mappings
{
    public class MembershipPlanMapper
    {
        public static MembershipPlanDto MapToDto(MembershipPlan plan)
        {
            return new MembershipPlanDto
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                DurationInMonths = plan.DurationInMonths
            };
        }
    }
}
