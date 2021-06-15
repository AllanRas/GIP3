using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Lekkerbek12Gip.Controllers
{
    [Authorize(Roles = "Admin,Chef")]
    public class ChefsController : Controller
    {
        private readonly IBestellingsService _bestellingsService;
        private readonly IChefsService _chefsService;
        public ChefsController(IBestellingsService bestellingsService, IChefsService chefsService)
        {

            _bestellingsService = bestellingsService;
            _chefsService = chefsService;
        }



        // GET: Bestellings/Edit/5
        public async Task<IActionResult> AddChef(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _bestellingsService.Get(x => x.BestellingId == id);

            if (bestelling == null)
            {
                return NotFound();
            }



            if (User.IsInRole("Chef"))
            {

                ViewData["ChefId"] = User.Identity.Name;
            }
            ViewData["ChefSelect"] = new SelectList(await _chefsService.GetList(), "ChefId", "ChefName");

            return View(bestelling);
        }

        // POST: AddChef/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddChef(int id, [Bind("BestellingId,ChefId,KlantId,SpecialeWensen,OrderDate,Afgerekend,AfhaalTijd,Korting,IsConfirmed")] Bestelling bestelling)
        {
            int numberOfChef = 0;
            if (bestelling.ChefId != null)
            {
                // numberOfChef = _chefsService.GetNumberOfBestellingForAChef(bestelling.ChefId, id);

            }


            if (id != bestelling.BestellingId)
            {
                return NotFound();
            }

            if (numberOfChef >= 4)
            {
                ModelState.AddModelError(nameof(bestelling.ChefId), "Geen tijslod voor deze Chef! " +
                    "Gelieve kies andere Chef");
            }

            if (ModelState.IsValid)
            {

                bestelling.IsConfirmed = true;
                bestelling.BestelingStatus = Bestelling.BestelStatus.GettingReady;
                await _bestellingsService.Update(bestelling);


                return RedirectToAction("AssignChef", "Chefs");
            }
            ViewData["ChefName"] = new SelectList(await _chefsService.GetList(), "ChefId", "ChefName");
            return View(bestelling);
        }


        // GET: Chefs

        public async Task<IActionResult> AssignChef()
        {

            return View(await _chefsService.GetChefIndexViewModel());

        }
        // GET: Chefs

        public async Task<IActionResult> Index()
        {

            return View(await _chefsService.GetList());

        }



        // GET: Chefs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _chefsService.Get(m => m.ChefId == id);

            if (chef == null)
            {
                return NotFound();
            }

            return View(chef);
        }

        // GET: Chefs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chefs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChefId,Email,ChefName")] Chef chef)
        {
            if (ModelState.IsValid)
            {
                await _chefsService.Add(chef);
                return RedirectToAction(nameof(Index));
            }
            return View(chef);
        }

        // GET: Chefs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _chefsService.Get(x => x.ChefId == id);
            if (chef == null)
            {
                return NotFound();
            }
            ViewData["Email"] = chef.Email;
            return View(chef);
        }

        // POST: Chefs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChefId,Email,ChefName")] Chef chef)
        {
            if (id != chef.ChefId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _chefsService.Update(chef);

                return RedirectToAction(nameof(Index));
            }
            ViewData["Email"] = chef.Email;
            return View(chef);
        }

        // GET: Chefs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chef = await _chefsService.Get(m => m.ChefId == id);

            if (chef == null)
            {
                return NotFound();
            }

            return View(chef);
        }

        // POST: Chefs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chef = await _chefsService.Get(x => x.ChefId == id);

            await _chefsService.Delete(chef);
            return RedirectToAction(nameof(Index));
        }

    }
}
