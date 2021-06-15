using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;

namespace Lekkerbek12Gip.Controllers
{
    public class PlanningsModulesController : Controller
    {
    
        private readonly IPlanningModuleService _planningService;
        private readonly IChefsService _chefsService;
        private readonly IEventsService _eventsService;



        public PlanningsModulesController(LekkerbekContext context, IPlanningModuleService planningService, IChefsService chefsService, IEventsService eventsService)
        {
          
            _planningService = planningService;
            _chefsService = chefsService;
            _eventsService = eventsService;
        }

        // GET: PlanningsModules
        public async Task<IActionResult> Index()
        {
                                 
            return View(await _planningService.GetAllBestellingwithInclude());
        }


        // GET: PlanningsModules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planningsModule = await _planningService.GetPlanningwithIncludeFilter(m => m.PlanningsModuleId == id);
            if (planningsModule == null)
            {
                return NotFound();
            }

            return View(planningsModule);
        }

        // GET: PlanningsModules/Create
        public async Task<IActionResult> Create()
        {
            ViewData["Chefs"] = await _chefsService.GetList();   
            return View();
        }

        // POST: PlanningsModules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanningsModuleId,ChefId,Description,OpeningsUren,SluitenUren")] PlanningsModule planningsModule, string[] statu)
        {

            if (await _planningService.Get(x => x.OpeningsUren.Date == planningsModule.OpeningsUren.Date) != null)
            {
                ModelState.AddModelError(nameof(planningsModule.OpeningsUren), "Er is al een plan posted");
            }
            if (ModelState.IsValid)
            {
                await _planningService.CreatePlan(planningsModule, statu);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Chefs"] = await _chefsService.GetList();       
            return View(planningsModule);
        }

        // GET: PlanningsModules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planningsModule = await _planningService.Get(x=>x.PlanningsModuleId==id);
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
                   await  _planningService.Update(planningsModule);
           
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PlanningsModuleExists(planningsModule.PlanningsModuleId))
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
                await _eventsService.Add(eventX);       
                return RedirectToAction(nameof(Index));
            }
            return View(eventX);
        }

        //Get Warning and
        public  List<DateTime> Warning()
        {
                 
            return  _planningService.Warning();
        }

        //Get Events
        public async Task<List<Event>> Event()
        {
            var events = await _eventsService.GetList();
            return events.ToList();
        }

        // GET: PlanningsModules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planningsModule = await _planningService.Get(m => m.PlanningsModuleId == id);
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
         
          var  planningsModule =await _planningService.GetPlanningwithIncludeFilter(x => x.PlanningsModuleId == id);
          await  _planningService.Delete(planningsModule);
        
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PlanningsModuleExists(int id)
        {
            var pl = await _planningService.Get(e => e.PlanningsModuleId == id);
            return pl==null?true:false;
        }
    }
}
