using FitnessClub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.Repositories
{
    public interface IMembershipPlanRepository
    {
        Task<MembershipPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<MembershipPlan>?> GetAllAsync(CancellationToken cancellationToken);
    }
}
