using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
   public interface IEntityRepositoryBase<T> where T:class ,new()
    {
        Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> filter = null);
        Task<T> Get(Expression<Func<T, bool>> filter = null);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);         
    }
}
