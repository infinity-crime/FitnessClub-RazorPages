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
        /// <summary>
        /// Получает абонемент по его id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>MembershipPlan?</returns>
        Task<MembershipPlan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Позволяет получить весь доступный список абонементов, отсортированный по цене (по возрастанию).
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>IEnumerable<MembershipPlan>?</returns>
        Task<IEnumerable<MembershipPlan>?> GetAllAsync(CancellationToken cancellationToken);
    }
}
