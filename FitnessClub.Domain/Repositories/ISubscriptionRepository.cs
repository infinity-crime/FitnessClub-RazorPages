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
        Task<Subscription?> GetByIdAsync(Guid id);

        Task<IEnumerable<Subscription>?> GetByUserIdAsync(Guid userId);

        Task Add(Subscription subscription);

        Task Update(Subscription subscription);
    }
}
