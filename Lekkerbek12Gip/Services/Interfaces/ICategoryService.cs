using Lekkerbek12Gip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
    public interface ICategoryService : IEntityRepositoryBase<Category>
    {
        Task<Category> GetCategoryWithIncludeFilter(Expression<Func<Category, bool>> filter);
    }
}
