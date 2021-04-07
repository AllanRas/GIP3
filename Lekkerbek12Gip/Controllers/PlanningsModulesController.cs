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
    public class PlanningsModulesController : Controller
    {
        private readonly LekkerbekContext _context;

        public PlanningsModulesController(LekkerbekContext context)
        {
            _context = context;
        }

        // GET: PlanningsModules
        public async Task<IActionResult> Index()
        {
            ViewData["ChefName"] = new SelectList(_context.Chefs, "ChefId", "ChefName");
            
            return View(await _context.PlanningsModules.ToListAsync());
        }

        // GET: PlanningsModules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planningsModule = await _context.PlanningsModules
                .FirstOrDefaultAsync(m => m.PlanningsModuleId == id);
            if (planningsModule == null)
            {
                return NotFound();
            }

            return View(planningsModule);
        }

        public IActionResult CreateEvent()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent([Bind("EventId,Title,Start,End")] Event eventX)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventX);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventX);
        }


        public async Task<List<Event>> Event()//Event
        {
            
           
            await _context.SaveChangesAsync();

            return await _context.Events.ToListAsync();
        }

        // GET: PlanningsModules/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlanningsModules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanningsModuleId,ChefId,OpeningsUren")] PlanningsModule planningsModule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planningsModule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(planningsModule);
        }

        // GET: PlanningsModules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planningsModule = await _context.PlanningsModules.FindAsync(id);
            if (planningsModule == null)
            {
                return NotFound();
            }
            return View(planningsModule);
        }

        // POST: PlanningsModules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanningsModuleId,ChefId,OpeningsUren")] PlanningsModule planningsModule)
        {
            if (id != planningsModule.PlanningsModuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planningsModule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanningsModuleExists(planningsModule.PlanningsModuleId))
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
            return View(planningsModule);
        }

        // GET: PlanningsModules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planningsModule = await _context.PlanningsModules
                .FirstOrDefaultAsync(m => m.PlanningsModuleId == id);
            if (planningsModule == null)
            {
                return NotFound();
            }

            return View(planningsModule);
        }

        // POST: PlanningsModules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var planningsModule = await _context.PlanningsModules.FindAsync(id);
            _context.PlanningsModules.Remove(planningsModule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanningsModuleExists(int id)
        {
            return _context.PlanningsModules.Any(e => e.PlanningsModuleId == id);
        }
    }
}
