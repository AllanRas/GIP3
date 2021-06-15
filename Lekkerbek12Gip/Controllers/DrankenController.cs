using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Models.Product;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Lekkerbek12Gip.Controllers
{
    [Authorize(Roles = "Admin,Kassamedewerker,Klant")]
    public class DrankenController : Controller
    {

        private readonly IDrankenService _drankenService;
        private readonly ICategoryService _categoryService;

        public DrankenController(LekkerbekContext context, IDrankenService drankenService, ICategoryService category)
        {
            _drankenService = drankenService;
            _categoryService = category;
        }

        [AllowAnonymous]
        // GET: Dranken
        public async Task<IActionResult> Index()
        {
            return View(await _drankenService.GetAllDrankenWithInclude());
        }

        // GET: Dranken/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drank = await _drankenService.GetDrankWithIncludeFilter(x => x.DrankId == id);
            if (drank == null)
            {
                return NotFound();
            }

            return View(drank);
        }

       
        // GET: Dranken/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetList(), "CategoryId", "Name");
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
                await _drankenService.Add(drank);
                return RedirectToAction("Index", "Gerechten");
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetList(), "CategoryId", "Name");
            return View(drank);
        }

        // GET: Dranken/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drank = await _drankenService.GetDrankWithIncludeFilter(x => x.DrankId == id);
            if (drank == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetList(), "CategoryId", "Name");
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

                await _drankenService.Update(drank);

                return RedirectToAction("Index", "Gerechten");
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetList(), "CategoryId", "Name", drank.CategoryId);
            return View(drank);
        }

        // GET: Dranken/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var drank = await _drankenService.GetDrankWithIncludeFilter(x => x.DrankId == id);

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
            var drank = await _drankenService.GetDrankWithIncludeFilter(x => x.DrankId == id);
            await _drankenService.Delete(drank);
            return RedirectToAction("Index", "Gerechten");
        }
    }
}
