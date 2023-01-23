using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.SharedKernel.Interface
{
    public interface IReadRepositoryBase<T> where T : class
    {
        Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default(CancellationToken)) where TId : notnull;

        Task<T?> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default(CancellationToken)) where Spec : ISingleResultSpecification, ISpecification<T>;


        Task<List<T>> ListAsync(CancellationToken cancellationToken = default(CancellationToken));



    }
}
