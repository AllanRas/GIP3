using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Models.Mails;
using Lekkerbek12Gip.Services.Interfaces;
using Lekkerbek12Gip.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class BesteldeGerechtenService : RepositoryBase<BestellingGerechten>, IBesteldeGerectenService
    {
        private readonly LekkerbekContext _context;
        private readonly IEmailService _emailService;
        public BesteldeGerechtenService(LekkerbekContext context, IEmailService emailService) : base(context)
        {
            _context = context;
            _emailService = emailService;

        }

        public async Task<BesteldeGerechtenIndexModel> besteldeGerechtenIndexModel(int Bestelid)
        {
            BesteldeGerechtenIndexModel bestelde = new BesteldeGerechtenIndexModel();
            bestelde.Gerechts = _context.Gerechten.ToList();
            bestelde.BestellingGerechten = (List<BestellingGerechten>)await GetAllBestellingGerechtenwithInclude();

            var klantid = _context.Bestellings.Include(x=>x.Klant).FirstOrDefault(x => x.BestellingId == Bestelid).KlantId;
            var klant = _context.Klants.Include(x => x.Fav).FirstOrDefault(x => x.KlantId == klantid);

            var toegevoegd= await GetAllBestellingGerechtenwithInclude();
            bestelde.ToegevoegdGerechtens = toegevoegd.Where(x => x.BestellingId == Bestelid).ToList();
            bestelde.favGerechten = klant.Fav.ToList();
            
           return  bestelde;
        }

        async public Task<string> ConfirmBestelling(int bestellingId, string specialeWensen)
        {

            var klant = _context.Bestellings.Include(x => x.Klant).FirstOrDefault(x => x.BestellingId == bestellingId).Klant;
            var bestelling = _context.Bestellings.Include("Gerechten").Include(x => x.Klant).FirstOrDefault(x => x.BestellingId == bestellingId);
            //var bg = _context.BestellingGerechten.Where(x => x.BestellingId == bestellingId);
            var bg =await GetBestellingGerechtenwithIncludeFilter(x => x.BestellingId == bestellingId);


            int totaalAantal = 0;
            foreach (BestellingGerechten b in bg)
            {
                totaalAantal += b.Aantal;
            }

            if (totaalAantal < 1)
            {
                bestelling.IsConfirmed = false;
                return "~/BesteldeGerechten/Gerechten/" + bestelling.BestellingId;
            }
            else
            {
                if (bestelling.IsConfirmed != true)
                {                  
                    _emailService.Send(new GemakteOrderMail { Bestellings = bg.ToList() }, klant);
                    bestelling.IsConfirmed = true;
                }

            }
            bestelling.SpecialeWensen = specialeWensen;
            _context.Update(bestelling);
            await _context.SaveChangesAsync();
            return "~/Bestellings/";
        }

        public async Task Gerechten(int bestellingId, int gerechtId, int aantal)
        {
            if (aantal < 0)
            {
                aantal = 0;
            }
            var bestelling = await _context.Bestellings.Include(x => x.Gerechten).Include(x => x.Klant).FirstOrDefaultAsync(x => x.BestellingId == bestellingId);
            var gerecht = await _context.Gerechten.FindAsync(gerechtId);

            var bestellinGerecht = _context.BestellingGerechten.Where(x => x.BestellingId == bestellingId);

            if (aantal > 0 && bestellinGerecht != null && bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId) == null)
            {
                BestellingGerechten bg = new BestellingGerechten
                {
                    Aantal = aantal,
                    Bestelling = bestelling,
                    BestellingId = bestelling.BestellingId,
                    Gerecht = gerecht,
                    GerechtId = gerecht.GerechtId
                };
                bestelling.Gerechten.Add(gerecht);
                bestelling.BestellingGerechten.Add(bg);
                await _context.BestellingGerechten.AddAsync(bg);

            }
            else if (bestellinGerecht != null && bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId) != null)
            {
                if (aantal == 0)
                {
                    var delete = await _context.BestellingGerechten.FirstAsync(x => (x.BestellingId == bestelling.BestellingId) && (x.GerechtId == gerecht.GerechtId));
                    _context.BestellingGerechten.Remove(delete);
                }
             
                bestellinGerecht.FirstOrDefault(x => x.GerechtId == gerecht.GerechtId).Aantal = aantal;
            }
            await _context.SaveChangesAsync();          
        }

        public async Task<IEnumerable<BestellingGerechten>> GetAllBestellingGerechtenwithInclude(ClaimsPrincipal user = null)
        {
            var bestellingList = _context.BestellingGerechten
                   .Include(x => x.Gerecht)
                   .Include(x => x.Bestelling);           
                return await bestellingList.ToListAsync();
        }

        public async Task<IEnumerable<BestellingGerechten>> GetBestellingGerechtenwithIncludeFilter(Expression<Func<BestellingGerechten, bool>> filter)
        {
            var bestelling = await _context.BestellingGerechten
                   .Include(x => x.Gerecht)
                   .OrderBy(x => x.Bestelling).Where(filter).ToListAsync();
            return bestelling;
        }



    }
}
