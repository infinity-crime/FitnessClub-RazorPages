using FitnessClub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<Subscription?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IEnumerable<Subscription>?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        Task Add(Subscription subscription, CancellationToken cancellationToken);

        Task Update(Subscription subscription, CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
