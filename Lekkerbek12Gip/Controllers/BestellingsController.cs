using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;

namespace Lekkerbek12Gip.Controllers
{
    public class BestellingsController : Controller
    {
        private readonly LekkerbekContext _context;

        public BestellingsController(LekkerbekContext context)
        {
            _context = context;
        }

        // GET: Bestellings
        public async Task<IActionResult> Index()
        {
            var lekkerbekContext = _context.Bestellings.Include("Gerechten").Include(x => x.Klant);
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
        //Gerech kies
        [HttpGet]
        public async Task<IActionResult> Gerechten(Bestelling bestelling)
        {
            ViewData["data"] = bestelling.BestellingId;
            return View(await _context.Gerechten.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> GerechPOST(IEnumerable<Gerecht> gerechts, int bestellingId)
        {

            foreach (var a in gerechts)
            {
                if (a.Aantal > 0)
                {
                    var gerecht = _context.Gerechten.FirstOrDefault(x => x.GerechtId == a.GerechtId);
                    gerecht.Aantal = a.Aantal;
                    _context.Bestellings.Include("Gerechten").FirstOrDefault(x => x.BestellingId == bestellingId).Gerechten.Add(gerecht);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // GET: Bestellings/Create
        public IActionResult Create()
        {
            ViewData["Name"] = new SelectList(_context.Klants, "KlantId", "Name");
            ViewData["ChefName"] = new SelectList(_context.Chefs, "ChefId", "ChefName");

            var lastHourChef2 = _context.Bestellings
                 .Where(p => (p.OrderDate > DateTime.Now && p.OrderDate < DateTime.Now.AddMinutes(60)) && p.ChefId == 2).Count();

            ViewBag.lastHourChef2 = 4 - lastHourChef2;



            var lastHourChef1 = _context.Bestellings
                 .Where(p => (p.OrderDate > DateTime.Now && p.OrderDate < DateTime.Now.AddMinutes(60)) && p.ChefId == 1).Count();

            ViewBag.lastHourChef1 = 4 - lastHourChef1;
            return View();
        }

        // POST: Bestellings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BestellingId,ChefId,KlantId,SpecialeWensen,OrderDate,Afgerekend,AfhaalTijd,Korting")] Bestelling bestelling)
        {

            if (ModelState.IsValid)
            {


                var bestellingCount = _context.Bestellings.Where(x => x.KlantId == bestelling.KlantId).Count();
                var klant = _context.Klants.FirstOrDefault(x => x.KlantId == bestelling.KlantId);

                if (klant != null)
                {
                    klant.GetrouwheidsScore += 1;
                }


                if (bestellingCount % 3 == 0)
                {
                    bestelling.Korting = 10;
                    
                }

                _context.Add(bestelling);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Gerechten), bestelling);
            }
            ViewData["KlantId"] = new SelectList(_context.Klants, "KlantId", "KlantId", bestelling.KlantId);
            ViewData["ChefId"] = new SelectList(_context.Chefs, "ChefId", "ChefId", bestelling.ChefId);


            return View(bestelling);
        }

        // GET: Bestellings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _context.Bestellings.FindAsync(id);

            if (bestelling == null)
            {
                return NotFound();
            }

            ViewData["KlantId"] = new SelectList(_context.Klants, "KlantId", "KlantId", bestelling.KlantId);
            ViewData["ChefId"] = new SelectList(_context.Chefs, "ChefId", "ChefId", bestelling.ChefId);
            return View(bestelling);
        }

        // POST: Bestellings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BestellingId,ChefId,KlantId,SpecialeWensen,OrderDate,Afgerekend,AfhaalTijd,Korting")] Bestelling bestelling)
        {
            if (id != bestelling.BestellingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestelling);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestellingExists(bestelling.BestellingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlantId"] = new SelectList(_context.Klants, "KlantId", "KlantId", bestelling.KlantId);
            ViewData["ChefId"] = new SelectList(_context.Chefs, "ChefId", "ChefId", bestelling.ChefId);
            return View(bestelling);
        }

        // GET: Bestellings/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Bestellings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestelling = await _context.Bestellings.FindAsync(id);
            _context.Bestellings.Remove(bestelling);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
