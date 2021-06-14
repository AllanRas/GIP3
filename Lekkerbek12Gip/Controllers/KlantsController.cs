using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;

namespace Lekkerbek12Gip.Controllers
{
    [Authorize(Roles = "Admin,Kassamedewerker")]
    public class KlantsController : Controller
    {
        private readonly LekkerbekContext _context;

        public KlantsController(LekkerbekContext context)
        {
            _context = context;
        }

        // GET: Klants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Klants.ToListAsync());
        }

        // GET: Klants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klants
                .FirstOrDefaultAsync(m => m.KlantId == id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // GET: Klants/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlantId,Name,Adress,GetrouwheidsScore,Geboortedatum,emailadres")] Klant klant,[Bind("FirmaNaam, BtwNummer")] Firma firma)
        {

            if (ModelState.IsValid)
            {
                klant.Firma = firma;
                firma.Klant = klant;
                firma.KlantId = klant.KlantId;
                _context.Add(firma);
                _context.Add(klant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klant);
        }

        // GET: Klants/Edit/5
        [AllowAnonymous]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null && User.IsInRole("Klant"))
            {
                var klantId =_context.Klants.FirstOrDefault(x => x.emailadres == User.Identity.Name).KlantId;
                id = klantId;
            }
            if (id == null)
            {
                return NotFound();
            }
            var firma = await _context.Firmas.FirstOrDefaultAsync(x => x.KlantId == id);
            var klant = await _context.Klants.FindAsync(id);
            klant.Firma = firma;
            if (klant == null)
            {
                return NotFound();
            }
            return View(klant);
        }

        // POST: Klants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KlantId,Name,Adress,GetrouwheidsScore,Geboortedatum,emailadres")] Klant klant, [Bind("FirmaNaam, BtwNummer")] Firma firma)
        {
            if (id != klant.KlantId && !User.IsInRole("Klant"))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var f = await _context.Firmas.FirstOrDefaultAsync(x => x.KlantId == klant.KlantId);
                    if (f != null) 
                    { 
                    f.BtwNummer = firma.BtwNummer;
                    f.FirmaNaam = firma.FirmaNaam;
                    }
                    _context.Update(f);
                    _context.Update(klant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlantExists(klant.KlantId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (User.IsInRole("Admin") || User.IsInRole("Kassamedewerker"))
                {
                    return RedirectToAction("Index", "Klants");
                }
                if (User.IsInRole("Klant"))
                {
                    return RedirectToAction("index", "Gerechten");
                }
                    return RedirectToAction(nameof(Index));
                }
            return View(klant);
        }

        // GET: Klants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klants
                .FirstOrDefaultAsync(m => m.KlantId == id);
            if (klant == null)
            {
                return NotFound();
            }

            return View(klant);
        }

        // POST: Klants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var klant = await _context.Klants.Include(x => x.Firma).FirstOrDefaultAsync(x => x.KlantId == id);
            if (klant != null)
            {
                _context.Klants.Remove(klant);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlantExists(int id)
        {
            return _context.Klants.Any(e => e.KlantId == id);
        }

        [HttpGet]
        public IActionResult LoadAllCustomers()
        {
            try
            {
                // getting all Customer data
                var customerData = (from c in _context.Klants
                                    select c).ToList<Klant>();
                //Returning Json Data
                return Json(new { data = customerData });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [AllowAnonymous]
        [HttpPost, ActionName("AddFav")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFav(int gerechtId, int bestellingId)
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
            return Redirect("~/BesteldeGerechten/Gerechten/" + bestId);
        }

        public async Task<IActionResult> DelFav(int gerechtId, int bestellingId)
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

            return Redirect("~/BesteldeGerechten/Gerechten/" + bestId);
        }



        [HttpPost]
        public IActionResult LoadAllCustomersSS()
        {
            try
            {
                int start = Convert.ToInt32(Request.Form["start"]);
                int length = Convert.ToInt32(Request.Form["length"]);
                string searchValue = Request.Form["search[value]"];
                string sortColumnName = Request.Form["columns[" +
               HttpContext.Request.Form["order[0][column]"] + "][name]"];
                string sortDirection = Request.Form["order[0][dir]"];
                var customerData = (from c in _context.Klants
                                    select c);
                int recordsTotal = customerData.Count();
                //search
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = (from c in _context.Klants
                                    where c.Name.ToLower().Contains(searchValue.ToLower())
                                   ||
                                    c.Adress.ToLower().Contains(searchValue.ToLower())                                
                                    
                                    select c);
                }
                //sorting
                customerData = customerData.OrderBy(sortColumnName + " " + sortDirection);
                //paging
                customerData = customerData.Skip(start).Take(length);
                //footer info
                int draw;
                int.TryParse(Request.Form["draw"], out draw);

                int recordsFiltered = customerData.Count();
                return Json(new
                {
                    data = customerData.ToList<Klant>(),
                    recordsTotal =
               recordsTotal,
                    recordsFiltered = recordsFiltered,
                    draw = draw
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
