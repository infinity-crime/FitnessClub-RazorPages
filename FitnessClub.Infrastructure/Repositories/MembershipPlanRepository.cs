using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using FitnessClub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Infrastructure.Repositories
{
    public class MembershipPlanRepository : IMembershipPlanRepository
    {
        private readonly ApplicationContext _dbContext;

        public MembershipPlanRepository(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MembershipPlan>?> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Plans
                .ToListAsync(cancellationToken);
        }

        public async Task<MembershipPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Plans
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
    }
}
