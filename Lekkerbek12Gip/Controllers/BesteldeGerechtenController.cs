using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Controllers
{
    public class BesteldeGerechtenController : Controller
    {
        private readonly LekkerbekContext _context;
        private readonly IEmailService _emailService;
        private readonly IBesteldeGerectenService _besteldeGerectenService;
        private readonly IGerechtenService _gerechtenService;

        public BesteldeGerechtenController(LekkerbekContext context, IEmailService emailService, IBesteldeGerectenService besteldeGerectenService, IGerechtenService gerechtenService)
        {
            _context = context;
            _emailService = emailService;
            _besteldeGerectenService = besteldeGerectenService;
            _gerechtenService = gerechtenService;
        }

        [HttpGet]
        public async Task<IActionResult> Gerechten(int? id)
        {
            List<BestellingGerechten> bestellingGerechten = await _context.BestellingGerechten.Include(b => b.Bestelling).Include(g => g.Gerecht).ToListAsync();
            Klant klant = await _context.Klants.FirstOrDefaultAsync(x => x.emailadres == User.Identity.Name);
            if (User.IsInRole("Klant"))
            {
                List<GerechtKlantFavoriet> gerechten = await _context.GerechtKlantFavorieten.Where(x => x.KlantId == klant.KlantId).ToListAsync();
                ViewData["FavGerechten"] = gerechten;
            }

            var bestellingGerecht = await _context.BestellingGerechten.Include(b => b.Bestelling).Include(g => g.Gerecht).Where(x => x.BestellingId == id).ToListAsync();
            ViewData["Toegevoegd"] = bestellingGerecht;

            ViewData["data"] = id;
            ViewData["Aantal"] = bestellingGerechten;

            return View(await _context.Gerechten.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Gerechten(int bestellingId, int gerechtId, int aantal)
        {

            var bestelling = await _context.Bestellings.Include(x => x.Gerechten).Include(x => x.Klant).FirstOrDefaultAsync(x => x.BestellingId == bestellingId);

            var gerecht = await _context.Gerechten.FindAsync(gerechtId);

            var bestellinGerecht = _context.BestellingGerechten.Where(x => x.BestellingId == bestellingId);

            if (aantal > 0 && bestellinGerecht != null && bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId) == null)
            {
                BestellingGerechten bg = new BestellingGerechten
                {
                    Aantal = aantal,
                    Bestelling = bestelling,
                    BestellingId = bestelling.BestellingId,
                    Gerecht = gerecht,
                    GerechtId = gerecht.GerechtId
                };
                bestelling.Gerechten.Add(gerecht);
                bestelling.BestellingGerechten.Add(bg);
                await _context.BestellingGerechten.AddAsync(bg);

            }
            else if (bestellinGerecht != null && bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId) != null)
            {
                if (aantal == 0)
                {
                    var delete = await _context.BestellingGerechten.FirstAsync(x => (x.BestellingId == bestelling.BestellingId) && (x.GerechtId == gerecht.GerechtId));
                    _context.BestellingGerechten.Remove(delete);
                }
                bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId).Aantal = aantal;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction();
        }
    }
}
