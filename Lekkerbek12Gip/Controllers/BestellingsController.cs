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
            var lekkerbekContext = _context.Bestellings.Include(x => x.Klant).Include("Gerechten").Include("Chef").Include(x => x.BestellingGerechten);
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
                .Include("Gerechten")
                .Include("Chef")
                .FirstOrDefaultAsync(m => m.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }
        //Gerech kies
        [HttpGet]
        public async Task<IActionResult> Gerechten(int? id)
        {
            ViewData["data"] = id;
            return View(await _context.Gerechten.ToListAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Gerechten(int bestellingId, int gerechtId, int aantal)
        {
            var bestelling = await _context.Bestellings.FindAsync(bestellingId);
            var gerecht = await _context.Gerechten.FindAsync(gerechtId);

            var bestellinGerecht = _context.BestellingGerechten.Where(x => x.BestellingId == bestellingId);

            if (bestellinGerecht != null && bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId) == null)
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
                bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId).Aantal = aantal;
            }

            //if (bestellinGerecht == null)
            //{
            //    BestellingGerechten bg = new BestellingGerechten
            //    {
            //        Aantal = aantal,
            //        Bestelling = bestelling,
            //        BestellingId = bestelling.BestellingId,
            //        Gerecht = gerecht,
            //        GerechtId = gerecht.GerechtId
            //    };
            //    bestelling.BestellingGerechten.Add(bg);
            //    await _context.BestellingGerechten.AddAsync(bg);

            //}

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Bestellings/Create
        [Authorize(Roles = "Admin,Kassamedewerker,Klant")]
        public IActionResult Create()
        {
           
                var klant = _context.Klants.FirstOrDefault(x => x.emailadres == User.Identity.Name);
                if (klant != null)
                    ViewData["Klant"] = klant;
                else if (User.IsInRole("Klant"))
                {
                Klant klant1 = new Klant { emailadres = User.Identity.Name };               
                ViewData["Klant"] = klant1;
                }
            else 
            {
                ViewData["Klant"] = new SelectList(_context.Klants, "KlantId", "Name"); 
            }
                        
            ViewData["GerechtData"] = _context.Gerechten.ToList();
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
                return RedirectToAction(nameof(Index));
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
        // GET: Bestellingen/Afrekenen/5
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

        public async Task<IActionResult> Factuur(int id)
        {
            Bestelling bestelling = await (from b in _context.Bestellings
                                    .Include(b => b.Klant)
                                    .Include(b => b.BestellingGerechten)
                                    .ThenInclude(bg => bg.Gerecht)
                                           where b.BestellingId == id
                                           select b).FirstOrDefaultAsync();
            if (bestelling == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["Prijs"] = bestelling.TotalPrijs;
                return View(bestelling);
            }
        }


        private bool BestellingExists(int id)
        {
            return _context.Bestellings.Any(e => e.BestellingId == id);
        }

    }
}
