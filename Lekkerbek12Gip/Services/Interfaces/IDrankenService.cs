using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
    public interface IDrankenService : IEntityRepositoryBase<Drank>
    {
        Task<IEnumerable<Drank>> GetAllDrankenWithInclude();
        Task<Drank> GetDrankWithIncludeFilter(Expression<Func<Drank, bool>> filter);
    }
}
