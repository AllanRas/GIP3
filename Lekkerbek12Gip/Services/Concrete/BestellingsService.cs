using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class BestellingsService : RepositoryBase<Bestelling>, IBestellingsService
    {
        private readonly LekkerbekContext _context;
        public BestellingsService(LekkerbekContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bestelling>> GetAllBestellingwithInclude(ClaimsPrincipal user = null)
        {
            var bestellingList = _context.Bestellings
                   .Include(x => x.Klant)
                   .Include(x => x.Gerechten)
                   .Include(x => x.Chef)
                   .Include(x => x.BestellingGerechten)
                   .OrderBy(x => x.Afgerekend)
                   .ThenByDescending(x => x.AfhaalTijd);

            if (user != null && user.IsInRole("Admin"))
            {
                return await bestellingList.ToListAsync();
            }
            return await bestellingList
                   .Where(x => x.Klant.emailadres == user.Identity.Name).ToListAsync();

        }

        public async Task<Bestelling> GetBestellingwithFilter(Expression<Func<Bestelling, bool>> filter)
        {
            var bestellingList = _context.Bestellings
               .Include(x => x.Klant)
               .Include(x => x.Gerechten)
               .Include(x => x.Chef)
               .Include(x => x.BestellingGerechten)
               .OrderBy(x => x.Afgerekend)
               .ThenByDescending(x => x.AfhaalTijd);


            return await bestellingList.FirstOrDefaultAsync(filter);

        }
    }
}
