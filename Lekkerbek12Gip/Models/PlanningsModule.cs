using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{   
    public class PlanningsModule
    {
        public PlanningsModule() 
        {
            if (chefs == null) 
            {
               chefs = new List<Chef>();
            }
            if (ChefPlanningsModules == null)
            {
                ChefPlanningsModules = new List<ChefPlanningsModule>();
            }
            if (Events == null)
            {
                Events = new List<Event>();
            }

        }
        public int PlanningsModuleId { get; set; }
        public int ChefId { get; set; }
        public string Description { get; set; }
        public ICollection<Bestelling> Bestellings { get; set; }
        public ICollection<Event> Events { get; set; }
        public DateTime OpeningsUren { get; set; }
        public DateTime SluitenUren { get; set; }

        public ICollection<Chef> chefs { get; set; }
        public ICollection<ChefPlanningsModule> ChefPlanningsModules { get; set; }

    }
}
