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
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationContext _dbContext;

        public SubscriptionRepository(ApplicationContext context)
        {
            _dbContext = context;
        }

        public async Task AddAsync(Subscription subscription, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(subscription, cancellationToken);
        }

        public Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Subscription>?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _dbContext.Subscriptions
                .Include(s => s.MembershipPlan)
                .Where(s => s.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public Task UpdateAsync(Subscription subscription, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
