using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.SharedKernel.Interface
{
    public interface IRepositoryBase<T> : IReadRepositoryBase<T> where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        Task SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
