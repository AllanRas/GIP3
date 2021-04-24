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
                        
            var indexlist = _context.PlanningsModules.Include(x => x.chefs).Include(x=>x.Bestellings).Include(x=>x.ChefPlanningsModules);          
            return View(await indexlist.ToListAsync());
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

        // GET: PlanningsModules/Create
        public IActionResult Create()
        {
            ViewData["Chefs"] = _context.Chefs.ToList();
            return View();
        }

        // POST: PlanningsModules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanningsModuleId,ChefId,Description,OpeningsUren")] PlanningsModule planningsModule, string[] statu)
        { 
         
            if (_context.PlanningsModules.FirstOrDefault(x => x.OpeningsUren.Date == planningsModule.OpeningsUren.Date) != null)
            {
                ModelState.AddModelError(nameof(planningsModule.OpeningsUren), "Er is al een plan posted");
            }            

            var list = _context.Bestellings
                .Where(x => x.OrderDate.Date == planningsModule.OpeningsUren.Date);
            planningsModule.Bestellings = list.ToList();

            var chefs = _context.Chefs.ToList(); 
            int i = 0;
           
                        
            if (ModelState.IsValid)
            {
                foreach (var item in chefs)
                    {               
                       if(statu[i] == "Werken") 
                        {                                        
                            ChefPlanningsModule chefPlanningsModule = new ChefPlanningsModule
                            {
                                Chef = item,
                                ChefStatu = ChefPlanningsModule.ChefStatus.Werken,
                                PlanningsModule=planningsModule
                            };
                        planningsModule.ChefPlanningsModules.Add(chefPlanningsModule);                        

                    }
                       else if(statu[i] == "Ziek") 
                            {
                            ChefPlanningsModule chefPlanningsModule = new ChefPlanningsModule
                            {
                                Chef = item,
                                ChefStatu = ChefPlanningsModule.ChefStatus.Ziek,
                                PlanningsModule = planningsModule
                            };
                        planningsModule.ChefPlanningsModules.Add(chefPlanningsModule);
                        
                    }
                       else if (statu[i] == "Toestemming")
                        {
                            ChefPlanningsModule chefPlanningsModule = new ChefPlanningsModule
                            {
                                Chef = item,
                                ChefStatu = ChefPlanningsModule.ChefStatus.Toestemming,
                                PlanningsModule = planningsModule
                            };
                        planningsModule.ChefPlanningsModules.Add(chefPlanningsModule);

                    }
                    
                        i++;
                    } //
                _context.Add(planningsModule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Chefs"] = _context.Chefs.ToList();
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
        public async Task<IActionResult> Edit(int id, [Bind("PlanningsModuleId,ChefId,Description,OpeningsUren")] PlanningsModule planningsModule)
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

        //Get Warning and
        public  List<DateTime> Warning()
        {
            var indexlist = _context.PlanningsModules
                .Include(x => x.chefs)
                .Include(x => x.Bestellings)
                .Include(x => x.ChefPlanningsModules);
                           
            List<DateTime> days = new List<DateTime>();
            foreach (var item in indexlist)
            {
              
                var chefsCount =item.ChefPlanningsModules
                    .Where(x=>x.ChefStatu==ChefPlanningsModule.ChefStatus.Werken)
                    .Count();

                if (item.Bestellings.Count > 0)
                {
                    var MaxBestellingPerHour = item.Bestellings
                   .GroupBy(x => x.OrderDate.Hour)
                   .Max(x => x.Count());
                    if (MaxBestellingPerHour > (chefsCount * 4))
                    {
                        days.Add(item.OpeningsUren.Date);
                    }
                }
            }          
            return days;
        }

        //Get Events
        public async Task<List<Event>> Event()
        {
            await _context.SaveChangesAsync();

            return await _context.Events.ToListAsync();
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
            var planningsModule = await _context.PlanningsModules
                .Include(x => x.chefs)
                .Include(x=>x.Bestellings)
                .FirstOrDefaultAsync(x => x.PlanningsModuleId == id); 
          
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
