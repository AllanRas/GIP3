﻿using Lekkerbek12Gip.Models;
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

        public BesteldeGerechtenController(IEmailService emailService, IBesteldeGerectenService besteldeGerectenService)
        {
            _emailService = emailService;
            _besteldeGerectenService = besteldeGerectenService;
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

            return RedirectToAction();
        }
        [AllowAnonymous]
        [HttpPost, ActionName("ConfirmBestelling")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBestelling(int bestellingId, string specialeWensen)
        {

            if (await _besteldeGerectenService.ConfirmBestelling(bestellingId, specialeWensen))
            {

                await _emailService.Send(new GemakteOrderMail(), bestellingId);
                return Redirect("~/BesteldeGerechten/Gerechten/" + bestellingId);

            }
            return Redirect("~/Bestellings/");
        }
    }
}
