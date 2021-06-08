using Lekkerbek12Gip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
    public interface IBestellingsService :IEntityRepositoryBase<Bestelling>
    {
       Task<IEnumerable<Bestelling>> GetAllBestellingwithInclude(ClaimsPrincipal user=null);
    }
}
