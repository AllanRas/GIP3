using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class ChefPlanningsModule
    {
        public enum ChefStatus
        {
            Werken,
            Ziek,
            Toestemming
        }
        public int? ChefId { get; set; }
        public int? PlanningsModuleId { get; set; }
        public virtual Chef Chef { get; set; }
        public virtual PlanningsModule PlanningsModule { get; set; }
        public ChefStatus ChefStatu { get; set; }
    }
}
