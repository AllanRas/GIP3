using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Lekkerbek12Gip.ViewModel.Chefs;
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
        private readonly IKlantsService _klantsService;
        public BestellingsService(LekkerbekContext context, IKlantsService klantsService) : base(context)
        {
            _context = context;
            _klantsService = klantsService;

        }

        public async Task<Bestelling> bestellingCreate(Bestelling bestelling)
        {
            var klant = await _klantsService.Get(x => x.KlantId == bestelling.KlantId);

            var bestellingCount = GetAllBestellingwithInclude(klant: klant).Result.Count();

            if (klant != null)
            {
                klant.GetrouwheidsScore += 1;
                bestelling.OrderDate = DateTime.Now;
                bestelling.IsConfirmed = false;
            }
            if (bestellingCount != 0 && (bestellingCount + 1) % 3 == 0)
            {
                bestelling.Korting = 10;
            }

            await Add(bestelling);
            return bestelling;

        }

        public async Task<IEnumerable<Bestelling>> GetAllBestellingwithInclude(ClaimsPrincipal user = null, Klant klant = null)
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
            else if (user != null && user.IsInRole("Klant"))
            {
                return await bestellingList.Where(x => x.Klant.emailadres == user.Identity.Name).ToListAsync();
            }
            return await bestellingList.Where(x => x.Klant.emailadres == klant.emailadres).ToListAsync();

        }

        public async Task<Bestelling> GetBestellingwithIncludeFilter(Expression<Func<Bestelling, bool>> filter)
        {
            var bestelling = await _context.Bestellings
                   .Include(x => x.Klant)
                   .Include(x => x.Gerechten)
                   .Include(x => x.Chef)
                   .Include(x => x.BestellingGerechten)
                   .OrderBy(x => x.Afgerekend)
                   .ThenByDescending(x => x.AfhaalTijd).FirstOrDefaultAsync(filter);
            return bestelling;
        }
        public async Task Afrekenen(Bestelling bestelling)
        {
            bestelling.Afgerekend = true;
            await _context.SaveChangesAsync();
        }


    }
}
