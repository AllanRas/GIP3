using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Bestelling
    {
        public Bestelling()
        {
            this.Gerechten = new HashSet<Gerecht>();
        }

        public enum SpecialWensen
        {
            Pikant = 1,
            Extra_groenten = 3,
            Extra_saus = 2
        }
        public int BestellingId { get; set; }
        public int? KlantId { get; set; }
        public int? ChefId { get; set; }
        public SpecialWensen? SpecialeWensen { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Afgerekend { get; set; }
        public DateTime AfhaalTijd { get; set; }
        public decimal Korting { get; set; }
        public decimal TotalPrijs
        {
            get
            {
                decimal totalPrijs = 0;
                if (Gerechten != null)
                {
                    foreach (var item in Gerechten)
                    {
                        totalPrijs += item.Prijs*Convert.ToDecimal(item.Aantal);
                    }
                    if (Korting == 10)
                    {
                        totalPrijs = totalPrijs * 9 / 10;
                    }
                }
                if (SpecialeWensen != null)
                {
                    totalPrijs += Convert.ToDecimal(SpecialeWensen);
                }
                return totalPrijs;
            }
        }         
        public virtual ICollection<Gerecht> Gerechten { get; set; }
        public virtual Klant Klant { get; set; }
        public virtual Chef Chef { get; set; }
    }
}
