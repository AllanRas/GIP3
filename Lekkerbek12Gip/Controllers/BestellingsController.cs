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

        private readonly IBestellingsService _service;
        private readonly LekkerbekContext _context;
        private readonly IEmailService _emailService;
        private readonly IKlantsService _klantsService;
        private readonly IEventsService _eventsService;
        private readonly IChefsService _chefsService;


        public BestellingsController(IBestellingsService service, LekkerbekContext context, IEmailService emailService, IKlantsService klantsService, IEventsService eventsService, IChefsService chefsService = null)
        {
            _service = service;
            _context = context;
            _emailService = emailService;
            _klantsService = klantsService;
            _eventsService = eventsService;
            _chefsService = chefsService;
        }

        // GET: Bestellings

        public async Task<IActionResult> Index()
        {
            //var list = await _service.GetList();
            return View(await _service.GetAllBestellingwithInclude(User));
        }

        // GET: Bestellings/AfgerekendeBestellingen
        public async Task<IActionResult> AfgerekendeBestellingen()
        {
            return View(await _service.GetAllBestellingwithInclude(User));
        }

        // GET: Bestellings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _service.GetBestellingwithIncludeFilter(x => x.BestellingId == id);
            if (bestelling == null)
            {
                return NotFound();
            }

            return View(bestelling);
        }



        //[HttpGet]
        //public async Task<IActionResult> Gerechten(int? id)
        //{
        //    List<BestellingGerechten> bestellingGerechten = await _context.BestellingGerechten.Include(b => b.Bestelling).Include(g => g.Gerecht).ToListAsync();
        //    Klant klant = await _context.Klants.FirstOrDefaultAsync(x => x.emailadres == User.Identity.Name);
        //    if (User.IsInRole("Klant"))
        //    {
        //        List<GerechtKlantFavoriet> gerechten = await _context.GerechtKlantFavorieten.Where(x => x.KlantId == klant.KlantId).ToListAsync();
        //        ViewData["FavGerechten"] = gerechten;
        //    }

        //    var bestellingGerecht = await _context.BestellingGerechten.Include(b => b.Bestelling).Include(g => g.Gerecht).Where(x => x.BestellingId == id).ToListAsync();
        //    ViewData["Toegevoegd"] = bestellingGerecht;

        //    ViewData["data"] = id;
        //    ViewData["Aantal"] = bestellingGerechten;

        //    return View(await _context.Gerechten.ToListAsync());
        //}

        //[HttpPost]
        //public async Task<IActionResult> Gerechten(int bestellingId, int gerechtId, int aantal)
        //{
        //    //var bestelling = await _context.Bestellings.FindAsync(bestellingId);
        //    var bestelling = await _service.GetBestellingwithIncludeFilter(x => x.BestellingId == bestellingId);
        //    var gerecht = await _context.Gerechten.FindAsync(gerechtId);

        //    var bestellinGerecht = _context.BestellingGerechten.Where(x => x.BestellingId == bestellingId);

        //    if (aantal > 0 && bestellinGerecht != null && bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId) == null)
        //    {
        //        BestellingGerechten bg = new BestellingGerechten
        //        {
        //            Aantal = aantal,
        //            Bestelling = bestelling,
        //            BestellingId = bestelling.BestellingId,
        //            Gerecht = gerecht,
        //            GerechtId = gerecht.GerechtId
        //        };
        //        bestelling.Gerechten.Add(gerecht);
        //        bestelling.BestellingGerechten.Add(bg);
        //        await _context.BestellingGerechten.AddAsync(bg);

        //    }
        //    else if (bestellinGerecht != null && bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId) != null)
        //    {
        //        if (aantal == 0)
        //        {
        //            var delete = await _context.BestellingGerechten.FirstAsync(x => (x.BestellingId == bestelling.BestellingId) && (x.GerechtId == gerecht.GerechtId));
        //            _context.BestellingGerechten.Remove(delete);
        //        }
        //        bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId).Aantal = aantal;
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction();
        //}

        // GET: Bestellings/Create
        public async Task<IActionResult> Create()
        {
            var klant = await _klantsService.Get(x => x.emailadres == User.Identity.Name);
                
            if (klant != null) ViewData["Klant"] = klant;
            // accounts made by register page automatically makes you klant so if else would not make appear klantselect list in dropdownlist
            ViewData["KlantSelect"] = new SelectList(await _klantsService.GetList(), "KlantId", "Name");
            //ViewData["GerechtData"] = _context.Gerechten.ToList();
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
                await _service.bestellingCreate(bestelling);
                                
                //var klant =await _klantsService.Get(x => x.KlantId == bestelling.KlantId);

                //var bestellingCount =_service.GetAllBestellingwithInclude(User).Result.Count();

                //if (klant != null)
                //{
                //    klant.GetrouwheidsScore += 1;
                //    bestelling.OrderDate = DateTime.Now;
                //    bestelling.IsConfirmed = false;
                //}
                //if (bestellingCount != 0 && (bestellingCount + 1) % 3 == 0)
                //{
                //    bestelling.Korting = 10;
                //}

                //_context.Add(bestelling);

                //await _context.SaveChangesAsync();

                if (User.IsInRole("Klant"))
                {
                    return RedirectToAction("Gerechten","BesteldeGerechten", new { id = bestelling.BestellingId });
                }
                ViewData["KlantSelect"] = new SelectList(await _klantsService.GetList(), "KlantId", "Name");
                return RedirectToAction(nameof(Index));

            }
            ViewData["KlantSelect"] = new SelectList(await _klantsService.GetList(), "KlantId", "Name");
            //ViewData["ChefId"] = new SelectList(await _chefsService.GetList(), "ChefId", "ChefId", bestelling.ChefId);
            return View(bestelling);
        }



        // GET: Bestellings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _service.GetBestellingwithIncludeFilter(x=>x.BestellingId==id);

            if (bestelling == null)
            {
                return NotFound();
            }

            //ViewData["KlantId"] = new SelectList(_context.Klants, "KlantId", "KlantId", bestelling.KlantId);
            //ViewData["ChefId"] = new SelectList(_context.Chefs, "ChefId", "ChefId", bestelling.ChefId);
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
                await _service.Update(bestelling);                
                return RedirectToAction(nameof(Index));
            }
            //ViewData["KlantId"] = new SelectList(await _klantsService.GetList(), "KlantId", "KlantId", bestelling.KlantId);          
            return View(bestelling);
        }

        // GET: Bestellings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var bestelling = await _context.Bestellings
            //    .Include(b => b.Klant)
            //    .FirstOrDefaultAsync(m => m.BestellingId == id);
           var bestelling =await _service.GetBestellingwithIncludeFilter(x => x.BestellingId == id);
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
            //var bestelling = await _context.Bestellings.FindAsync(id);
            //_context.Bestellings.Remove(bestelling);
            var bestelling = await _service.GetBestellingwithIncludeFilter(x => x.BestellingId == id);
             await _service.Delete(bestelling);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Bestellingen/Afrekenen/5
        public async Task<IActionResult> Afrekenen(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bestelling = await _service.GetBestellingwithIncludeFilter(x => x.BestellingId == id);
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
            var bestelling = await _service.GetBestellingwithIncludeFilter(x => x.BestellingId == id);
            //var bestellingGerechten = await _context.BestellingGerechten.Include(x => x.Bestelling).Include(x => x.Gerecht).Where(x => x.BestellingId == id).ToListAsync();
            
            bestelling.Afgerekend = true;
            //var klant = _context.Klants.FirstOrDefault(x => x.emailadres == User.Identity.Name);
            //var kalnt = _context.Users.FirstOrDefault(x => x.Email == bestelling.Klant.emailadres);
            
            //var klantUser = _context.Klants.FirstOrDefault(x => x.emailadres == bestelling.Klant.emailadres);
           
                
                await _emailService.Send(new BevestigMail(),id);

                //MailMessage mail = new MailMessage();
                //mail.To.Add(klant.emailadres);
                //mail.From = new MailAddress("lekkerbek12gip2@gmail.com");
                //mail.Subject = "Order";
                //mail.Body = "<h1 style = \"color: green\">Uw bestelling is bevestigd!</h1>";
                //mail.IsBodyHtml = true;
                //SmtpClient smtp = new SmtpClient();
                //smtp.Host = "smtp.gmail.com";
                //smtp.Port = 587;
                //smtp.UseDefaultCredentials = false;
                //smtp.Credentials = new System.Net.NetworkCredential("lekkerbek12gip2@gmail.com", "LekkerbekGip2");
                //smtp.EnableSsl = true;
                //smtp.Send(mail);
            
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Factuur(int id)
        {
            Bestelling bestelling = await (from b in _context.Bestellings
                                    .Include(b => b.Klant)
                                    .Include(b => b.BestellingGerechten)
                                    .ThenInclude(bg => bg.Gerecht)
                                           where b.BestellingId == id
                                           select b).FirstOrDefaultAsync();
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


        //[AllowAnonymous]
        //[HttpPost, ActionName("AddFav")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddFav(int gerechtId, int bestellingId)
        //{
        //    var gerecht = _context.Gerechten.FirstOrDefault(x => x.GerechtId == gerechtId);
        //    var klant = _context.Klants.FirstOrDefault(x => x.emailadres == User.Identity.Name);
        //    var favGerecht = await _context.GerechtKlantFavorieten.FirstOrDefaultAsync(x => x.KlantId == klant.KlantId && x.GerechtId == gerecht.GerechtId);
        //    var bestId = _context.Bestellings.FirstOrDefault(x => x.BestellingId == bestellingId).BestellingId;

        //    if (favGerecht == null)
        //    {
        //        GerechtKlantFavoriet klantFavoriet = new GerechtKlantFavoriet
        //        {
        //            GerechtId = gerecht.GerechtId,
        //            KlantId = klant.KlantId,
        //            Gerecht = gerecht,
        //            Klant = klant
        //        };
        //        _context.GerechtKlantFavorieten.Add(klantFavoriet);

        //    }
        //    await _context.SaveChangesAsync();
        //    return Redirect("~/BesteldeGerechten/Gerechten/" + bestId);
        //}

        //public async Task<IActionResult> DelFav(int gerechtId, int bestellingId)
        //{
        //    var gerecht = _context.Gerechten.FirstOrDefault(x => x.GerechtId == gerechtId);
        //    var klant = _context.Klants.FirstOrDefault(x => x.emailadres == User.Identity.Name);
        //    var bestId = _context.Bestellings.FirstOrDefault(x => x.BestellingId == bestellingId).BestellingId;

        //    var favGerecht = await _context.GerechtKlantFavorieten.FirstOrDefaultAsync(x => x.KlantId == klant.KlantId && x.GerechtId == gerecht.GerechtId);

        //    if (favGerecht != null)
        //    {
        //        _context.GerechtKlantFavorieten.Remove(favGerecht);
        //    }
        //    await _context.SaveChangesAsync();

        //    return Redirect("~/Bestellings/Gerechten/" + bestId);
        //}

        private bool BestellingExists(int id)
        {
            return _context.Bestellings.Any(e => e.BestellingId == id);
        }

        //[AllowAnonymous]
        //[HttpPost, ActionName("ConfirmBestelling")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ConfirmBestelling(int bestellingId, string specialeWensen)
        //{
        //    var klant = new Klant();
        //    if (User.IsInRole("Klant"))
        //    {
        //        klant = _context.Klants.FirstOrDefault(x => x.emailadres == User.Identity.Name);

        //    }
        //    else
        //    {
        //        klant = _context.Bestellings.Include("Klant").FirstOrDefault(x => x.BestellingId == bestellingId).Klant;
        //    }
        //    var bestelling = _context.Bestellings.Include("Gerechten").Include(x => x.Klant).FirstOrDefault(x => x.BestellingId == bestellingId);
        //    var bg = _context.BestellingGerechten.Where(x => x.BestellingId == bestellingId);


        //    int totaalAantal = 0;
        //    foreach (BestellingGerechten b in bg)
        //    {
        //        totaalAantal += b.Aantal;
        //    }

        //    if (totaalAantal < 1)
        //    {
        //        bestelling.IsConfirmed = false;
        //        return Redirect("~/Bestellings/Gerechten/" + bestelling.BestellingId);
        //    }
        //    else
        //    {
        //        if (bestelling.IsConfirmed != true)
        //        {
        //            var bestellingGerechten = await _context.BestellingGerechten.Include(x => x.Bestelling).Include(x => x.Gerecht).Where(x => x.BestellingId == bestellingId).ToListAsync();
        //            var klantUser = _context.Klants.FirstOrDefault(x => x.emailadres == bestelling.Klant.emailadres);

        //            _emailService.Send(new GemakteOrderMail { Bestellings = bestellingGerechten }, klantUser);
        //            bestelling.IsConfirmed = true;
        //        }

        //    }
        //    bestelling.SpecialeWensen = specialeWensen;
        //    _context.Update(bestelling);
        //    await _context.SaveChangesAsync();
        //    return Redirect("~/Bestellings/");
        //}

        public void SendMailBeforeAfhaaltijd(Klant klant)
        {

        }

        public void SendMailBevestigings(Klant klant)
        {

            //MailMessage mail = new MailMessage();

            //mail.To.Add(klant.emailadres);
            //mail.From = new MailAddress("lekkerbek12gip2@gmail.com");
            //mail.Subject = "Order";
            //mail.Body = "<h1 style = \"color:green\">Bestelling wordt aangemaakt. Je bestelling wordt binnenkort bevestigd!</h1>";
            //mail.IsBodyHtml = true;
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.UseDefaultCredentials = false;
            //smtp.Credentials = new System.Net.NetworkCredential("lekkerbek12gip2@gmail.com", "LekkerbekGip2");
            //smtp.EnableSsl = true;
            //smtp.Send(mail);
        }

    }
}
