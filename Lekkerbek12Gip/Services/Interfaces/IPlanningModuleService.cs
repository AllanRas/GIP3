using Lekkerbek12Gip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Services.Interfaces
{
    public interface IPlanningModuleService:IEntityRepositoryBase<PlanningsModule>
    {
        Task<IEnumerable<PlanningsModule>> GetAllBestellingwithInclude();
        Task<PlanningsModule> GetPlanningwithIncludeFilter(Expression<Func<PlanningsModule, bool>> Filter);
    }
}
