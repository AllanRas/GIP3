using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;
using System.Linq.Dynamic.Core;

namespace Lekkerbek12Gip.Controllers
{
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlantId,Name,Adress,GetrouwheidsScore,Geboortedatum,emailadres")] Klant klant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klant);
        }

        // GET: Klants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.Klants.FindAsync(id);
            if (klant == null)
            {
                return NotFound();
            }
            return View(klant);
        }

        // POST: Klants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("KlantId,Name,Adress,GetrouwheidsScore,Geboortedatum,emailadres")] Klant klant)
        {
            if (id != klant.KlantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klant = await _context.Klants.FindAsync(id);
            _context.Klants.Remove(klant);
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
