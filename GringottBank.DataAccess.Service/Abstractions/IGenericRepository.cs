using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GringottBank.DataAccess.Service.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> All();
        Task<bool> Add(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Upsert(T entity);
        Task<IList<T>> Find(Expression<Func<T, bool>> predicate);        
    }
}
