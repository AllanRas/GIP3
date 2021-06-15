using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Concrete
{
    public class PlanningModuleService:RepositoryBase<PlanningsModule>, IPlanningModuleService
    {
        private readonly LekkerbekContext _context;

        public PlanningModuleService(LekkerbekContext context):base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlanningsModule>> GetAllBestellingwithInclude()
        {
          return await  _context.PlanningsModules
                .Include(x=>x.Events)
                .Include(x=>x.chefs)
                .Include(x=>x.Bestellings)
                .Include(x=>x.ChefPlanningsModules).ToListAsync();
        }

        public async Task<PlanningsModule> GetPlanningwithIncludeFilter(Expression<Func<PlanningsModule, bool>> Filter)
        {
            return await _context.PlanningsModules
                .Include(x => x.Events)
                .Include(x => x.chefs)
                .Include(x => x.Bestellings)
                .Include(x => x.ChefPlanningsModules).FirstOrDefaultAsync(Filter);
        }
    }
}
