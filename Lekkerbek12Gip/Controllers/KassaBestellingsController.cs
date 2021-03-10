using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;
using Microsoft.AspNetCore.Authorization;

namespace Lekkerbek12Gip.Controllers
{
    [AllowAnonymous]
    public class KassaBestellingsController : Controller
    {
        private readonly LekkerbekContext _context;

        public KassaBestellingsController(LekkerbekContext context)
        {
            _context = context;
        }

        // GET: Bestellings
        public async Task<IActionResult> Index()
        {
            var lekkerbekContext = _context.Bestellings.Include(x => x.Klant).Include("Gerechten").Include("Chef");
            return View(await lekkerbekContext.ToListAsync());
        }

        // GET: Bestellings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestellings
                .Include(b => b.Klant)
                .FirstOrDefaultAsync(m => m.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }



        public async Task<IActionResult> Afrekenen(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestellings
                .Include(b => b.Klant)
                .FirstOrDefaultAsync(m => m.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }


        // POST: Bestellingen/Afrekenen/5
        [HttpPost, ActionName("Afrekenen")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Afrekenen(int id)
        {
            var bestelling = await _context.Bestellings.FindAsync(id);
            bestelling.Afgerekend = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool BestellingExists(int id)
        {
            return _context.Bestellings.Any(e => e.BestellingId == id);
        }

    }
}

