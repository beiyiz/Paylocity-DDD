using Microsoft.EntityFrameworkCore;
using Paylocity.Deduction.SharedKernel.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.Infrastructure.Data
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbContext dbContext;

       

        
        public RepositoryBase(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        
        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().Add(entity);

            await SaveChangesAsync(cancellationToken);

            return entity;
        }
        
        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Entry(entity).State = EntityState.Modified;

            await SaveChangesAsync(cancellationToken);
        }
        
        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().Remove(entity);

            await SaveChangesAsync(cancellationToken);
        }
        
        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            dbContext.Set<T>().RemoveRange(entities);

            await SaveChangesAsync(cancellationToken);
        }
        
        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        
        public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
        {
            return await dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
        }
        public virtual async Task<T?> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default(CancellationToken)) where Spec : ISpecification<T>, ISingleResultSpecification
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
        }


        public virtual async Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().ToListAsync(cancellationToken);
        }
       
    }

}
