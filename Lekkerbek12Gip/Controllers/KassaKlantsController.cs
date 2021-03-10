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
    public class KassaKlantsController : Controller
    {
        private readonly LekkerbekContext _context;

        public KassaKlantsController(LekkerbekContext context)
        {
            _context = context;
        }

        // GET: Klants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Klants.ToListAsync());
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

       

       

        private bool KlantExists(int id)
        {
            return _context.Klants.Any(e => e.KlantId == id);
        }
    }
}
