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
        /// <summary>
        /// Возвращает список всех доступных планов абонементов на сайте.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<IEnumerable<MembershipPlanDto>>> AllPlansAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Получает абонемент по его id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Result<MembershipPlanDto>> PlanByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
