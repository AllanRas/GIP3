using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Bestelling
    {
        public int Id { get; set; }
        public DateTime AfhaalTijd { get; set; }
        public int? KlantId { get; set; }
        public string SpecialeWensen { get; set; }
        public bool Afgerekend { get; set; }
        public decimal Korting {
            get
            {
                if (Klant.GetrouwheidsScore > 3)
                {
                    return 0.10m;
                }
                return 0.00m;
            }
        }
        public decimal TotalPrijs
        {
            get
            {
                decimal totalprice = 0.00m;
                foreach(Gerecht gerecht in Gerechten)
                {
                    totalprice += gerecht.Prijs;
                }
                return totalprice;
            }
        }
        public virtual ICollection<Gerecht> Gerechten { get; set; }
        public virtual Klant Klant { get; set; }
    }
}
