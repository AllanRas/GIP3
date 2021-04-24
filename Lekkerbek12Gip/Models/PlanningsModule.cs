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
        public int PlanningsModuleDate // gun ay ve yil olarak yapilabilir
        { 
            get         
            {
                    DateTime date = DateTime.Now;
               string day =  date.Day.ToString();
               string month = date.Month.ToString();
               string year = date.Year.ToString();
               return Convert.ToInt32(day+month+year);
            }
        }
        public int ChefId { get; set; }
        public string Description { get; set; }
        public ICollection<Bestelling> Bestellings { get; set; }
        public ICollection<Event> Events { get; set; }
        public DateTime OpeningsUren { get; set; }//Event varsa eklenemeyecek
        public ICollection<Chef> chefs { get; set; } // o gun calisacak chefler Event tarihi icindeyse chefler izinli olucak
        public ICollection<ChefPlanningsModule> ChefPlanningsModules { get; set; }

    }
}
