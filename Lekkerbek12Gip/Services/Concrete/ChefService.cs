using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Lekkerbek12Gip.ViewModel.Chefs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class ChefService : RepositoryBase<Chef>, IChefsService
    {
        private readonly LekkerbekContext _context;

        public ChefService(LekkerbekContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ChefIndexViewModel> GetChefIndexViewModel()
        {
            ChefIndexViewModel chefIndexViewModel = new ChefIndexViewModel
            {
                BestellingsNotReadyList = _context.Bestellings.Include(x => x.Gerechten).Include(x => x.Chef)
                .Where(x => x.BestelingStatus == Bestelling.BestelStatus.NotReady)
                .OrderBy(x => x.AfhaalTijd).ToList(),

                BestellingsReadyList = _context.Bestellings.Include(x => x.Gerechten).Include(x => x.Chef)
                .Where(x => x.BestelingStatus == Bestelling.BestelStatus.GettingReady)
                .OrderBy(x => x.AfhaalTijd).ToList(),

                BestellingsDoneList = _context.Bestellings.Include(x => x.Gerechten).Include(x => x.Chef)
                .Where(x => x.BestelingStatus == Bestelling.BestelStatus.Done)
                .OrderBy(x => x.AfhaalTijd).ToList(),
            };

            return chefIndexViewModel;
        }
        public int GetNumberOfChefs(Expression<Func<Bestelling, bool>> filter = null)
        {
            if (filter == null)
            {
                return _context.Chefs.Count();
            }
            else
            {
                return _context.Chefs.Where(filter).Count();
            }

        }
    }
}
