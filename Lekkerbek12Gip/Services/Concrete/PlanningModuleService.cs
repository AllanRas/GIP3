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

        public async Task CreatePlan(PlanningsModule planningsModule,string[] statu)
        {
            var list = _context.Bestellings
                 .Where(x => x.AfhaalTijd.Date == planningsModule.OpeningsUren.Date);
            planningsModule.Bestellings = list.ToList();

            var chefs = _context.Chefs.ToList();
            int i = 0;
            foreach (var item in chefs)
            {
                if (statu[i] == "Werken")
                {
                    ChefPlanningsModule chefPlanningsModule = new ChefPlanningsModule
                    {
                        Chef = item,
                        ChefStatu = ChefPlanningsModule.ChefStatus.Werken,
                        PlanningsModule = planningsModule
                    };
                    planningsModule.ChefPlanningsModules.Add(chefPlanningsModule);

                }
                else if (statu[i] == "Ziek")
                {
                    ChefPlanningsModule chefPlanningsModule = new ChefPlanningsModule
                    {
                        Chef = item,
                        ChefStatu = ChefPlanningsModule.ChefStatus.Ziek,
                        PlanningsModule = planningsModule
                    };
                    planningsModule.ChefPlanningsModules.Add(chefPlanningsModule);

                }
                else if (statu[i] == "Toestemming")
                {
                    ChefPlanningsModule chefPlanningsModule = new ChefPlanningsModule
                    {
                        Chef = item,
                        ChefStatu = ChefPlanningsModule.ChefStatus.Toestemming,
                        PlanningsModule = planningsModule
                    };

                    planningsModule.ChefPlanningsModules.Add(chefPlanningsModule);

                }

                i++;
            } //
            await Add(planningsModule);
            
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

        public List<DateTime> Warning()
        {
            var indexlist = _context.PlanningsModules
               .Include(x => x.chefs)
               .Include(x => x.Bestellings)
               .Include(x => x.ChefPlanningsModules).ToList();

            List<DateTime> days = new List<DateTime>();
            foreach (var item in indexlist)
            {

                var chefsCount = item.ChefPlanningsModules
                    .Where(x => x.ChefStatu == ChefPlanningsModule.ChefStatus.Werken)
                    .Count();

                if (item.Bestellings.Count > 0)
                {
                    var MaxBestellingPerHour = item.Bestellings
                   .GroupBy(x => x.AfhaalTijd.Hour)
                   .Max(x => x.Count());
                    if (MaxBestellingPerHour > (chefsCount * 4))
                    {
                        days.Add(item.OpeningsUren.Date);
                    }
                }
            }
            return  days;
        }
    }
}
