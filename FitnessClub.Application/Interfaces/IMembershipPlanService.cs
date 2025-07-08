using FitnessClub.Application.Common;
using FitnessClub.Application.DTOs;
using FitnessClub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Application.Interfaces
{
    public interface IMembershipPlanService
    {
        Task<Result<IEnumerable<MembershipPlanDto>>> AllPlansAsync(CancellationToken cancellationToken);

        Task<Result<MembershipPlanDto>> PlanByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
