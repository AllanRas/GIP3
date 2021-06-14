using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Lekkerbek12Gip.Controllers
{
    public class ChefsController : Controller
    {
        private readonly LekkerbekContext _context;
        private readonly IBestellingsService _bestellingsService;
        private readonly IChefsService _chefsService;
        public ChefsController(LekkerbekContext context, IBestellingsService bestellingsService, IChefsService chefsService)
        {
            _context = context;
            _bestellingsService = bestellingsService;
            _chefsService = chefsService;
        }



        // GET: Bestellings/Edit/5
        public async Task<IActionResult> AddChef(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _bestellingsService.Get(x => x.BestellingId == id);

            if (bestelling == null)
            {
                return NotFound();
            }



            if (User.IsInRole("Chef"))
            {

                ViewData["ChefId"] = User.Identity.Name;
            }
            ViewData["ChefSelect"] = new SelectList(_context.Chefs.ToList(), "ChefId", "ChefName");

            return View(bestelling);
        }

        // POST: AddChef/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddChef(int id, [Bind("BestellingId,ChefId,KlantId,SpecialeWensen,OrderDate,Afgerekend,AfhaalTijd,Korting,IsConfirmed")] Bestelling bestelling)
        {
            int numberOfChef = 0;
            if (bestelling.ChefId != null)
            {
                // numberOfChef = _chefsService.GetNumberOfBestellingForAChef(bestelling.ChefId, id);

            }


            if (id != bestelling.BestellingId)
            {
                return NotFound();
            }

            if (numberOfChef >= 4)
            {
                ModelState.AddModelError(nameof(bestelling.ChefId), "Geen tijslod voor deze Chef! " +
                    "Gelieve kies andere Chef");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    bestelling.IsConfirmed = true;
                    bestelling.BestelingStatus = Bestelling.BestelStatus.GettingReady;
                    await _bestellingsService.Update(bestelling);
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
            ViewData["ChefName"] = new SelectList(await _chefsService.GetList(), "ChefId", "ChefName");
            return View(bestelling);
        }


        //public async Task<IActionResult> Leveren(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var bestelling = await _bestellingsService.GetBestellingwithIncludeFilter(x => x.BestellingId == id);

        //    if (bestelling == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(bestelling);
        //}


        //// POST: Bestellingen/Leveren/5
        //[HttpPost, ActionName("Leveren")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Leveren(int id)
        //{
        //    var bestelling = await _bestellingsService.Get(x => x.BestellingId == id);

        //    bestelling.BestelingStatus = Bestelling.BestelStatus.Done;
        //   await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


        // GET: Chefs

        public async Task<IActionResult> Index()
        {

            //var timeOneDay = DateTime.Today.AddDays(1).AddSeconds(-1);

            //var lekkerbekContext = _context.Bestellings.Include(x => x.Gerechten).Include(x => x.Chef).OrderBy(x => x.AfhaalTijd);
            ////var lekkerbekContext = _context.Bestellings.Include(x => x.Gerechten).Include(x => x.Chef).Where(x => x.OrderDate <= timeOneDay && x.OrderDate >= DateTime.Today).OrderBy(x => x.OrderDate);

            return View(await _chefsService.GetChefIndexViewModel());
        }

        private bool BestellingExists(int id)
        {
            return _context.Bestellings.Any(e => e.BestellingId == id);
        }


        // GET: Chefs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chefs
                .FirstOrDefaultAsync(m => m.ChefId == id);
            if (chef == null)
            {
                return NotFound();
            }

            return View(chef);
        }

        // GET: Chefs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chefs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChefId,ChefName")] Chef chef)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chef);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chef);
        }

        // GET: Chefs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chefs.FindAsync(id);
            if (chef == null)
            {
                return NotFound();
            }
            return View(chef);
        }

        // POST: Chefs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChefId,ChefName")] Chef chef)
        {
            if (id != chef.ChefId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chef);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChefExists(chef.ChefId))
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
            return View(chef);
        }

        // GET: Chefs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _context.Chefs
                .FirstOrDefaultAsync(m => m.ChefId == id);
            if (chef == null)
            {
                return NotFound();
            }

            return View(chef);
        }

        // POST: Chefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chef = await _context.Chefs.FindAsync(id);
            _context.Chefs.Remove(chef);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChefExists(int id)
        {
            return _context.Chefs.Any(e => e.ChefId == id);
        }
    }
}
