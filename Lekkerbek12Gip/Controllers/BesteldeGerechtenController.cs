using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Models.Mails;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Controllers
{
    public class BesteldeGerechtenController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IBesteldeGerectenService _besteldeGerectenService;
        private readonly IKlantsService _klantsService;

        public BesteldeGerechtenController(IEmailService emailService, IBesteldeGerectenService besteldeGerectenService, IKlantsService klantsService)
        {
            _emailService = emailService;
            _besteldeGerectenService = besteldeGerectenService;
            _klantsService = klantsService;
        }

        [HttpGet]
        public async Task<IActionResult> Gerechten(int id)
        {

            var ViewModel = await _besteldeGerectenService.besteldeGerechtenIndexModel(id);

            ViewData["data"] = id;
            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Gerechten(int bestellingId, int gerechtId, int aantal)
        {
            await _besteldeGerectenService.Gerechten(bestellingId, gerechtId, aantal);
            ViewData["data"] = bestellingId;
            return RedirectToAction();
        }

        [HttpPost]
        public async Task<IActionResult> Dranken(int bestellingId, int drankId, int aantal)
        {
            await _besteldeGerectenService.Dranken(bestellingId, drankId, aantal);
            ViewData["data"] = bestellingId;
            return RedirectToAction("Gerechten", new { id = bestellingId });
        }
        [AllowAnonymous]
        [HttpPost, ActionName("ConfirmBestelling")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBestelling(int bestellingId, string specialeWensen)
        {

            if (await _besteldeGerectenService.ConfirmBestelling(bestellingId, specialeWensen))
            {
                if (User.IsInRole("Klant"))
                {
                    var klant = await _klantsService.Get(x => x.emailadres.ToLower() == User.Identity.Name.ToLower());
                    return Redirect("~/Reviews/Create/" + klant.KlantId);
                }

                await _emailService.Send(new GemakteOrderMail(), bestellingId);
                return Redirect("~/BesteldeGerechten/Gerechten/" + bestellingId);

            }
            return Redirect("~/Bestellings/");
        }
    }
}
