using Ardalis.Specification.EntityFrameworkCore;
using Paylocity.Deduction.SharedKernel.Interface;

namespace Paylocity.Deduction.Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
