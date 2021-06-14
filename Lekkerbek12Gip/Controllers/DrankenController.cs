using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Models.Product;

namespace Lekkerbek12Gip.Controllers
{
    public class DrankenController : Controller
    {
        private readonly LekkerbekContext _context;

        public DrankenController(LekkerbekContext context)
        {
            _context = context;
        }

        // GET: Dranken
        public async Task<IActionResult> Index()
        {
            var lekkerbekContext = _context.Dranken.Include(d => d.Category);
            return View(await lekkerbekContext.ToListAsync());
        }

        // GET: Dranken/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drank = await _context.Dranken
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.DrankId == id);
            if (drank == null)
            {
                return NotFound();
            }

            return View(drank);
        }

        // GET: Dranken/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View();
        }

        // POST: Dranken/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DrankId,Name,Omschrijving,CategoryId,Prijs")] Drank drank)
        {
            if (ModelState.IsValid)
            {
                //var cat= _context.Categories.FirstOrDefault(x => x.CategoryId == drank.CategoryId);
                // drank.Category = cat;
                _context.Add(drank);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Gerechten");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "Name");
            return View(drank);
        }

        // GET: Dranken/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drank = await _context.Dranken.FindAsync(id);
            if (drank == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View(drank);
        }

        // POST: Dranken/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DrankId,Name,CategoryId,Prijs")] Drank drank)
        {
            if (id != drank.DrankId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(drank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DrankExists(drank.DrankId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Gerechten");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", drank.CategoryId);
            return View(drank);
        }

        // GET: Dranken/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drank = await _context.Dranken
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.DrankId == id);
            if (drank == null)
            {
                return NotFound();
            }

            return View(drank);
        }

        // POST: Dranken/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var drank = await _context.Dranken.FindAsync(id);
            _context.Dranken.Remove(drank);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Gerechten");
        }

        private bool DrankExists(int id)
        {
            return _context.Dranken.Any(e => e.DrankId == id);
        }
    }
}
