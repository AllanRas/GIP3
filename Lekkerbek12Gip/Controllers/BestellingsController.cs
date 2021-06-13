using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lekkerbek12Gip.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using Lekkerbek12Gip.Services.Interfaces;
using Lekkerbek12Gip.Models.Mails;

namespace Lekkerbek12Gip.Controllers
{
    [Authorize(Roles = "Admin,Kassamedewerker,Klant")]
    public class BestellingsController : Controller
    {

        private readonly IBestellingsService _bestellingsService;
        private readonly IEmailService _emailService;
        private readonly IKlantsService _klantsService;
        private readonly IEventsService _eventsService;


        public BestellingsController(IBestellingsService service, IEmailService emailService, IKlantsService klantsService, IEventsService eventsService)
        {
            _bestellingsService = service;
            _emailService = emailService;
            _klantsService = klantsService;
            _eventsService = eventsService;
        }

        // GET: Bestellings

        public async Task<IActionResult> Index()
        {
           
            return View(await _bestellingsService.GetAllBestellingwithInclude(User));
        }

        // GET: Bestellings/AfgerekendeBestellingen
        public async Task<IActionResult> AfgerekendeBestellingen()
        {
            return View(await _bestellingsService.GetAllBestellingwithInclude(User));
        }

        // GET: Bestellings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _bestellingsService.GetBestellingwithIncludeFilter(x => x.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // GET: Bestellings/Create
        public async Task<IActionResult> Create()
        {
            var klant = await _klantsService.Get(x => x.emailadres == User.Identity.Name);

            if (klant != null) ViewData["Klant"] = klant;
            // accounts made by register page automatically makes you klant so if else would not make appear klantselect list in dropdownlist
            ViewData["KlantSelect"] = new SelectList(await _klantsService.GetList(), "KlantId", "Name");
            return View();
        }

        // POST: Bestellings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BestellingId,ChefId,KlantId,SpecialeWensen,OrderDate,Afgerekend,AfhaalTijd,Korting")] Bestelling bestelling)
        {
            var events = await _eventsService.GetList();
            foreach (var item in events)
            {
                //We check if the order placed doesn't fall under an event, if the afhaaltijd of the order falls on an event
                //it will not be able to confirm the order.
                //We don't use orderdate because we should still be able to place an order when there is an event
                //you shouldn't be able to PICK UP an order when there is an event taking place
                if (bestelling.AfhaalTijd > item.Start && bestelling.AfhaalTijd < item.End)
                    ModelState.AddModelError(nameof(bestelling.AfhaalTijd), "afhaaltijd is geplaatst tijdens een event, gelieve een andere tijd te nemen.");
            }
            if (ModelState.IsValid)
            {
                if (bestelling.AfhaalTijd < DateTime.Now)
                {
                    return NotFound();
                }
                await _bestellingsService.bestellingCreate(bestelling);

                if (User.IsInRole("Klant"))
                {
                    return RedirectToAction("Gerechten", "BesteldeGerechten", new { id = bestelling.BestellingId });
                }
                ViewData["KlantSelect"] = new SelectList(await _klantsService.GetList(), "KlantId", "Name");
                return RedirectToAction(nameof(Index));

            }
            var klant = await _klantsService.Get(x => x.emailadres == User.Identity.Name);
            if (klant != null) ViewData["Klant"] = klant;
            ViewData["KlantSelect"] = new SelectList(await _klantsService.GetList(), "KlantId", "Name");
            return View(bestelling);
        }



        // GET: Bestellings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _bestellingsService.GetBestellingwithIncludeFilter(x => x.BestellingId == id);

            if (bestelling == null)
            {
                return NotFound();
            }

            ViewData["klantNaam"] = bestelling.Klant;
            return View(bestelling);
        }

        // POST: Bestellings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BestellingId,ChefId,KlantId,SpecialeWensen,OrderDate,Afgerekend,AfhaalTijd")] Bestelling bestelling)
        {

            if (id != bestelling.BestellingId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _bestellingsService.Update(bestelling);
                return RedirectToAction(nameof(Index));
            }
            return View(bestelling);
        }

        // GET: Bestellings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _bestellingsService.GetBestellingwithIncludeFilter(x => x.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }

        // POST: Bestellings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bestelling = await _bestellingsService.GetBestellingwithIncludeFilter(x => x.BestellingId == id);
            await _bestellingsService.Delete(bestelling);
            return RedirectToAction(nameof(Index));
        }
        // GET: Bestellingen/Afrekenen/5
        public async Task<IActionResult> Afrekenen(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _bestellingsService.GetBestellingwithIncludeFilter(x => x.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }


        // POST: Bestellingen/Afrekenen/5
        [HttpPost, ActionName("Afrekenen")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Afrekenen(int id)
        {
            var bestelling = await _bestellingsService.GetBestellingwithIncludeFilter(x => x.BestellingId == id);

            await _emailService.Send(new BevestigMail(), id);
            await _bestellingsService.Afrekenen(bestelling);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Factuur(int id)
        {
            var bestelling = await _bestellingsService.GetBestellingwithIncludeFilter(x => x.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["Prijs"] = bestelling.TotalPrijs;
                return View(bestelling);
            }
        }

        public async Task<IActionResult> Leveren(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _bestellingsService.GetBestellingwithIncludeFilter(x => x.BestellingId == id);

            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }


        // POST: Bestellingen/Leveren/5
        [HttpPost, ActionName("Leveren")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Leveren(int id)
        {
            var bestelling = await _bestellingsService.Get(x => x.BestellingId == id);

            bestelling.BestelingStatus = Bestelling.BestelStatus.Done;
            await _bestellingsService.Update(bestelling);
            return RedirectToAction(nameof(Index));
        }


        private async Task<bool> BestellingExists(int id)
        {
            var result = await _bestellingsService.Get(x => x.BestellingId == id);
            return result == null ? false : true;

        }

    }
}
