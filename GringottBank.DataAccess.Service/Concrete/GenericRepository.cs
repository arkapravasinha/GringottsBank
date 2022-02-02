using GringottBank.DataAccess.Service.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GringottBank.DataAccess.Service.Concrete
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly ILogger _logger;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext dbContext, ILogger logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<IList<T>> All()
        {
           return await _dbSet.ToListAsync<T>();
        }

        public virtual Task<bool> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<IList<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public virtual Task<bool> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
