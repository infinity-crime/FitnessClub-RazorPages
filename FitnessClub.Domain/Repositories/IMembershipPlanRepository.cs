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

        Task AddAsync(MembershipPlan plan, CancellationToken cancellationToken);

        Task Update(MembershipPlan plan, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
