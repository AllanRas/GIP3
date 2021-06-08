using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class BestellingsService:RepositoryBase<Bestelling>,IBestellingsService   
    {
        private readonly LekkerbekContext _context;
        public BestellingsService(LekkerbekContext context) :base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bestelling>> GetAllBestellingwithInclude(ClaimsPrincipal user=null)
        {
            if (user!=null &&user.IsInRole("Admin")) 
            {
                return await _context.Bestellings
                   .Include(x => x.Klant)
                   .Include(x => x.Gerechten)
                   .Include(x => x.Chef)
                   .Include(x => x.BestellingGerechten)                  
                   .OrderBy(x => x.Afgerekend)
                   .ThenByDescending(x => x.AfhaalTijd).ToListAsync();   
            }
            return await _context.Bestellings
                   .Include(x => x.Klant)
                   .Include(x => x.Gerechten)
                   .Include(x => x.Chef)
                   .Include(x => x.BestellingGerechten)
                   .OrderBy(x => x.Afgerekend)
                   .ThenByDescending(x => x.AfhaalTijd)
                   .Where(x => x.Klant.emailadres == user.Identity.Name).ToListAsync();

        }
    }
}
