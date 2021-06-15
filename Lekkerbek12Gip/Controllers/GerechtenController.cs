using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;
using Microsoft.AspNetCore.Authorization;
using Lekkerbek12Gip.Services.Interfaces;

namespace Lekkerbek12Gip.Controllers
{

    [Authorize(Roles = "Admin,Kassamedewerker")]
    public class GerechtenController : Controller
    {
        private readonly IGerechtenService _gerechtenService;
        private readonly ICategoryService _categoryService;

        public GerechtenController(IGerechtenService gerechtenService, ICategoryService categoryService)
        {
            _gerechtenService = gerechtenService;
            _categoryService = categoryService;
        }

        // GET: Gerechten
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _gerechtenService.GerechtenIndexModel());
        }

        // GET: Gerechten/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gerecht = await _gerechtenService.Get(m => m.GerechtId == id);
            if (gerecht == null)
            {
                return NotFound();
            }

            return View(gerecht);
        }

        // GET: Gerechten/Create

        public async Task<IActionResult> Create()
        {
            ViewData["Categories"] = new SelectList(await _categoryService.GetList(), "CategoryId", "Name");
            return View();
        }

        // POST: Gerechten/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GerechtId,Naam,Omschrijving,Prijs,CategoryId")] Gerecht gerecht)
        {
            if (ModelState.IsValid)
            {
                await _gerechtenService.Add(gerecht);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categories"] = new SelectList(await _categoryService.GetList(), "CategoryId", "Name");
            return View(gerecht);
        }

        // GET: Gerechten/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gerecht = await _gerechtenService.Get(m => m.GerechtId == id);
            if (gerecht == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetList(), "CategoryId", "CategoryId");
            return View(gerecht);
        }

        // POST: Gerechten/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GerechtId,Naam,Omschrijving,Prijs,CategoryId")] Gerecht gerecht)
        {
            if (id != gerecht.GerechtId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                await _gerechtenService.Update(gerecht);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetList(), "CategoryId", "CategoryId");
            return View(gerecht);
        }

        // GET: Gerechten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gerecht = await _gerechtenService.GetGerechtWithIncludeFilter(x => x.GerechtId == id);
            if (gerecht == null)
            {
                return NotFound();
            }

            return View(gerecht);
        }

        // POST: Gerechten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gerecht = await _gerechtenService.GetGerechtWithIncludeFilter(x => x.GerechtId == id);
            await _gerechtenService.Delete(gerecht);
            return RedirectToAction(nameof(Index));
        }

    }
}
