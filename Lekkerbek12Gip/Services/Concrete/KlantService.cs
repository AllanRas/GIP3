using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class KlantService : RepositoryBase<Klant>, IKlantsService
    {
        private readonly LekkerbekContext _context;
        public KlantService(LekkerbekContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddFav(int gerechtId, int bestellingId)
        {
            var gerecht = _context.Gerechten.FirstOrDefault(x => x.GerechtId == gerechtId);
            var klantid = _context.Bestellings.Include(x => x.Klant).FirstOrDefault(x => x.BestellingId == bestellingId).KlantId;
            var klant = _context.Klants.Include(x => x.Fav).FirstOrDefault(x => x.KlantId == klantid);
            var favGerecht = await _context.GerechtKlantFavorieten.FirstOrDefaultAsync(x => x.KlantId == klant.KlantId && x.GerechtId == gerecht.GerechtId);
            var bestId = _context.Bestellings.FirstOrDefault(x => x.BestellingId == bestellingId).BestellingId;

            if (favGerecht == null)
            {
                GerechtKlantFavoriet klantFavoriet = new GerechtKlantFavoriet
                {
                    GerechtId = gerecht.GerechtId,
                    KlantId = klant.KlantId,
                    Gerecht = gerecht,
                    Klant = klant
                };
                _context.GerechtKlantFavorieten.Add(klantFavoriet);
                klant.Fav.Add(gerecht);

            }
            await _context.SaveChangesAsync();
        }

        public async Task DelFav(int gerechtId, int bestellingId)
        {
            var gerecht = _context.Gerechten.FirstOrDefault(x => x.GerechtId == gerechtId);
            var klantid = _context.Bestellings.Include(x => x.Klant).FirstOrDefault(x => x.BestellingId == bestellingId).KlantId;
            var klant = _context.Klants.Include(x => x.Fav).FirstOrDefault(x => x.KlantId == klantid);
            var bestId = _context.Bestellings.FirstOrDefault(x => x.BestellingId == bestellingId).BestellingId;

            var favGerecht = await _context.GerechtKlantFavorieten.FirstOrDefaultAsync(x => x.KlantId == klant.KlantId && x.GerechtId == gerecht.GerechtId);

            if (favGerecht != null)
            {
                _context.GerechtKlantFavorieten.Remove(favGerecht);
            }
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Klant> GetKlantenIE()
        {
            return _context.Klants.AsEnumerable();
        }



    }
}
