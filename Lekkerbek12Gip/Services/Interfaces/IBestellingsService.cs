using Lekkerbek12Gip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
    public interface IBestellingsService : IEntityRepositoryBase<Bestelling>
    {
        Task<IEnumerable<Bestelling>> GetAllBestellingwithInclude(ClaimsPrincipal user = null,Klant klant=null);
        Task<Bestelling> GetBestellingwithIncludeFilter(Expression<Func<Bestelling, bool>> filter);
        Task<Bestelling> bestellingCreate(Bestelling bestelling);
    }
}
