using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Models.Product;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class DrankenService : RepositoryBase<Drank>, IDrankenService
    {
        private readonly LekkerbekContext _context;
        public DrankenService(LekkerbekContext context) : base(context)
        {
            _context = context;

        }

        public async Task<IEnumerable<Drank>> GetAllDrankenWithInclude()
        {
            var dranken = await _context.Dranken.Include(d => d.Category).ToListAsync();

            return dranken;
        }

        public async Task<Drank> GetDrankWithIncludeFilter(Expression<Func<Drank, bool>> filter)
        {
            var drank = await _context.Dranken
                .Include(d => d.Category)
                .FirstOrDefaultAsync(filter);
            return drank;
        }
    }
}
