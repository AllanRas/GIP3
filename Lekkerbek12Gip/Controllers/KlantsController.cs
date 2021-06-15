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
using Lekkerbek12Gip.Services.Interfaces;

namespace Lekkerbek12Gip.Controllers
{
    [Authorize(Roles = "Admin,Kassamedewerker")]
    public class KlantsController : Controller
    {
        private readonly LekkerbekContext _context;
        private readonly IKlantsService _klantService;
        private readonly IFirmaService _firmaService;
        public KlantsController(LekkerbekContext context, IKlantsService klantService, IFirmaService firmaService)
        {
            _context = context;
            _klantService = klantService;
            _firmaService = firmaService;
        }

        // GET: Klants
        public async Task<IActionResult> Index()
        {
            return View(await _klantService.GetList());
        }

        // GET: Klants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _klantService.Get(m => m.KlantId == id);

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
        public async Task<IActionResult> Create([Bind("KlantId,Name,Adress,GetrouwheidsScore,Geboortedatum,emailadres")] Klant klant, [Bind("FirmaNaam, BtwNummer")] Firma firma)
        {

            if (ModelState.IsValid)
            {
                firma.Klant = klant;
                klant.Firma = firma;
                //firma.KlantId = klant.KlantId;
                await _klantService.Add(klant);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Klants/Edit/5
        [AllowAnonymous]
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null && User.IsInRole("Klant"))
            {
                id = _klantService.Get(x => x.emailadres == User.Identity.Name).Result.KlantId;
            }

            var firma = await _firmaService.Get(x => x.KlantId == id);
            var klant = await _klantService.Get(x => x.KlantId == id);
            
            if (klant == null)
            {
                return NotFound();
            }
            klant.Firma = firma;
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
            if (User.IsInRole("Klant"))
            {
                id = _klantService.Get(x => x.emailadres == User.Identity.Name).Result.KlantId;
            }

            if (ModelState.IsValid)
            {
                var updateKlant = await _klantService.Get(x => x.KlantId == id);
                updateKlant.Adress = klant.Adress;
                updateKlant.Geboortedatum = klant.Geboortedatum;
                updateKlant.Name = klant.Name;

                var f = await _firmaService.Get(x => x.KlantId == id);
                if (f != null)
                {
                    f.BtwNummer = firma.BtwNummer;
                    f.FirmaNaam = firma.FirmaNaam;
                    await _firmaService.Update(f);
                }
                else
                {
                    await _firmaService.Add(firma);
                }
                await _klantService.Update(updateKlant);
                
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

            var klant = await _klantService
                .Get(m => m.KlantId == id);
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
            var klant = await _klantService.Get(x => x.KlantId == id);
            var firma = await _firmaService.Get(x => x.KlantId == id);

            if (klant != null)
            {
                await _firmaService.Delete(firma);
                await _klantService.Delete(klant);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool KlantExists(int id)
        {
            return _context.Klants.Any(e => e.KlantId == id);
        }

        [HttpGet]
        public async Task<IActionResult> LoadAllCustomers()
        {
            try
            {
                // getting all Customer data
                var customerData = await _klantService.GetList();

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

            await _klantService.AddFav(gerechtId, bestellingId);
            return Redirect("~/BesteldeGerechten/Gerechten/" + bestellingId);
        }

        public async Task<IActionResult> DelFav(int gerechtId, int bestellingId)
        {

            await _klantService.DelFav(gerechtId, bestellingId);

            return Redirect("~/BesteldeGerechten/Gerechten/" + bestellingId);
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
