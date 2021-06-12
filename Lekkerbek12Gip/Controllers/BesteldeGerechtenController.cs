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
        private readonly LekkerbekContext _context;
        private readonly IEmailService _emailService;
        private readonly IBesteldeGerectenService _besteldeGerectenService;
        private readonly IGerechtenService _gerechtenService;

        public BesteldeGerechtenController(LekkerbekContext context, IEmailService emailService, IBesteldeGerectenService besteldeGerectenService, IGerechtenService gerechtenService)
        {
            _context = context;
            _emailService = emailService;
            _besteldeGerectenService = besteldeGerectenService;
            _gerechtenService = gerechtenService;
        }

        [HttpGet]
        public async Task<IActionResult> Gerechten(int id)
        {
            //var bestellingGerechten = await _besteldeGerectenService.GetAllBestellingGerechtenwithInclude();
          
            var ViewModel = await _besteldeGerectenService.besteldeGerechtenIndexModel(id);
            //  await _context.BestellingGerechten.Include(b => b.Bestelling).Include(g => g.Gerecht).ToListAsync();
            //Klant klant = await _context.Klants.FirstOrDefaultAsync(x => x.emailadres == User.Identity.Name);
            //if (User.IsInRole("Klant"))
            //{
            //    List<GerechtKlantFavoriet> gerechten = await _context.GerechtKlantFavorieten.Where(x => x.KlantId == klant.KlantId).ToListAsync();
            //   
            //}
            //ViewData["FavGerechten"] = deneme.favGerechten;
            //var bestellingGerecht = await _context.BestellingGerechten.Include(b => b.Bestelling).Include(g => g.Gerecht).Where(x => x.BestellingId == id).ToListAsync();
            //ViewData["Toegevoegd"] = bestellingGerecht;

            ViewData["data"] = id;
            //ViewData["Aantal"] = deneme.BestellingGerechten;

            return View(ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Gerechten(int bestellingId, int gerechtId, int aantal)
        {
           await _besteldeGerectenService.Gerechten(bestellingId, gerechtId, aantal);

            //var bestelling = await _context.Bestellings.Include(x => x.Gerechten).Include(x => x.Klant).FirstOrDefaultAsync(x => x.BestellingId == bestellingId);

            //var gerecht = await _context.Gerechten.FindAsync(gerechtId);

            //var bestellinGerecht = _context.BestellingGerechten.Where(x => x.BestellingId == bestellingId);

            //if (aantal > 0 && bestellinGerecht != null && bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId) == null)
            //{
            //    BestellingGerechten bg = new BestellingGerechten
            //    {
            //        Aantal = aantal,
            //        Bestelling = bestelling,
            //        BestellingId = bestelling.BestellingId,
            //        Gerecht = gerecht,
            //        GerechtId = gerecht.GerechtId
            //    };
            //    bestelling.Gerechten.Add(gerecht);
            //    bestelling.BestellingGerechten.Add(bg);
            //    await _context.BestellingGerechten.AddAsync(bg);

            //}
            //else if (bestellinGerecht != null && bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId) != null)
            //{
            //    if (aantal == 0)
            //    {
            //        var delete = await _context.BestellingGerechten.FirstAsync(x => (x.BestellingId == bestelling.BestellingId) && (x.GerechtId == gerecht.GerechtId));
            //        _context.BestellingGerechten.Remove(delete);
            //    }
            //    bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId).Aantal = aantal;
            //}

            //await _context.SaveChangesAsync();
            return RedirectToAction();
        }
        [AllowAnonymous]
        [HttpPost, ActionName("ConfirmBestelling")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmBestelling(int bestellingId, string specialeWensen)
        {


            //var klant = new Klant();
            //if (User.IsInRole("Klant"))
            //{
            //    klant = _context.Klants.FirstOrDefault(x => x.emailadres == User.Identity.Name);

            //}
            //else
            //{
            //    klant = _context.Bestellings.Include("Klant").FirstOrDefault(x => x.BestellingId == bestellingId).Klant;
            //}
            //var bestelling = _context.Bestellings.Include("Gerechten").Include(x => x.Klant).FirstOrDefault(x => x.BestellingId == bestellingId);
            //var bg = _context.BestellingGerechten.Where(x => x.BestellingId == bestellingId);


            //int totaalAantal = 0;
            //foreach (BestellingGerechten b in bg)
            //{
            //    totaalAantal += b.Aantal;
            //}

            //if (totaalAantal < 1)
            //{
            //    bestelling.IsConfirmed = false;
            //    return Redirect("~/Bestellings/Gerechten/" + bestelling.BestellingId);
            //}
            //else
            //{
            //    if (bestelling.IsConfirmed != true)
            //    {
            //        var bestellingGerechten = await _context.BestellingGerechten.Include(x => x.Bestelling).Include(x => x.Gerecht).Where(x => x.BestellingId == bestellingId).ToListAsync();
            //        var klantUser = _context.Klants.FirstOrDefault(x => x.emailadres == bestelling.Klant.emailadres);

            //        //_emailService.Send(new GemakteOrderMail { Bestellings = bestellingGerechten }, klantUser);
            //        bestelling.IsConfirmed = true;
            //    }

            //}
            //bestelling.SpecialeWensen = specialeWensen;
            //_context.Update(bestelling);
            //await _context.SaveChangesAsync();
            if(await _besteldeGerectenService.ConfirmBestelling(bestellingId, specialeWensen)) 
            {

                await _emailService.Send(new GemakteOrderMail(), bestellingId);
                return Redirect("~/BesteldeGerechten/Gerechten/" + bestellingId);
                
            }
            return Redirect("~/Bestellings/");
        }
    }
}
