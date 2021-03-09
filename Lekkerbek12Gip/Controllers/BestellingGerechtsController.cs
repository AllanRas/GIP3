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
    public class BestellingGerechtsController : Controller
    {
        private readonly LekkerbekContext _context;

        public BestellingGerechtsController(LekkerbekContext context)
        {
            _context = context;
        }

        // GET: BestellingGerecht
        public async Task<IActionResult> Index()
        {
            var lekkerbekContext = _context.BestellingGerecht.Include(b => b.Bestelling).Include(b => b.Gerecht);
            return View(await lekkerbekContext.ToListAsync());
        }

        // GET: BestellingGerecht/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestellingGerecht = await _context.BestellingGerecht
                .Include(b => b.Bestelling)
                .Include(b => b.Gerecht)
                .FirstOrDefaultAsync(m => m.GerechtId == id);
            if (bestellingGerecht == null)
            {
                return NotFound();
            }

            return View(bestellingGerecht);
        }

        // GET: BestellingGerecht/Create
        public IActionResult Create()
        {
            ViewData["BestellingId"] = new SelectList(_context.Bestellings, "BestellingId", "BestellingId");
            ViewData["GerechtId"] = new SelectList(_context.Gerechten, "GerechtId", "Naam");
            return View();
        }

        // POST: BestellingGerecht/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BestellingId,GerechtId,Aantal")] BestellingGerecht bestellingGerecht)
        {

            if (ModelState.IsValid)
            {
                _context.Add(bestellingGerecht);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BestellingId"] = new SelectList(_context.Bestellings, "BestellingId", "BestellingId", bestellingGerecht.BestellingId);
            ViewData["GerechtId"] = new SelectList(_context.Gerechten, "GerechtId", "GerechtId", bestellingGerecht.GerechtId);
            return View(bestellingGerecht);
        }

        // GET: BestellingGerecht/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestellingGerecht = await _context.BestellingGerecht.FindAsync(id);
            if (bestellingGerecht == null)
            {
                return NotFound();
            }
            ViewData["BestellingId"] = new SelectList(_context.Bestellings, "BestellingId", "BestellingId", bestellingGerecht.BestellingId);
            ViewData["GerechtId"] = new SelectList(_context.Gerechten, "GerechtId", "GerechtId", bestellingGerecht.GerechtId);
            return View(bestellingGerecht);
        }

        // POST: BestellingGerecht/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BestellingId,GerechtId,Aantal")] BestellingGerecht bestellingGerecht)
        {
            if (id != bestellingGerecht.GerechtId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bestellingGerecht);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BestellingGerechtExists(bestellingGerecht.GerechtId))
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
            ViewData["BestellingId"] = new SelectList(_context.Bestellings, "BestellingId", "BestellingId", bestellingGerecht.BestellingId);
            ViewData["GerechtId"] = new SelectList(_context.Gerechten, "GerechtId", "GerechtId", bestellingGerecht.GerechtId);
            return View(bestellingGerecht);
        }

        // GET: BestellingGerecht/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestellingGerecht = await _context.BestellingGerecht
                .Include(b => b.Bestelling)
                .Include(b => b.Gerecht)
                .FirstOrDefaultAsync(m => m.GerechtId == id);
            if (bestellingGerecht == null)
            {
                return NotFound();
            }

            return View(bestellingGerecht);
        }

        // POST: BestellingGerecht/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestellingGerecht = await _context.BestellingGerecht.FindAsync(id);
            _context.BestellingGerecht.Remove(bestellingGerecht);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BestellingGerechtExists(int? id)
        {
            return _context.BestellingGerecht.Any(e => e.GerechtId == id);
        }
    }
}
