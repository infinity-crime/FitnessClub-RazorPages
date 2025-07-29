using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessClub.Domain.Repositories
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Дублирует метод сохранения изменений EF Core для сервиса, где недоступен контекст БД.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
