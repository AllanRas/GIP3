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
            return View(await _context.klants.ToListAsync());
        }

        // GET: Klants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klant = await _context.klants
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
        public async Task<IActionResult> Create([Bind("KlantId,Name")] Klant klant)
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

            var klant = await _context.klants.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("KlantId,Name")] Klant klant)
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

            var klant = await _context.klants
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
            var klant = await _context.klants.FindAsync(id);
            _context.klants.Remove(klant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlantExists(int id)
        {
            return _context.klants.Any(e => e.KlantId == id);
        }
    }
}
