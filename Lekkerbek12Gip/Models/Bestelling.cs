using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Bestelling
    {
        public int Id { get; set; }
        public int? KlantId { get; set; }
        public string SpecialeWensen { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Afgerekend { get; set; }
        public DateTime AfhaalTijd { get; set; }
        public decimal Korting {get; set; }
        public decimal TotalPrijs{ get; set; }
        public virtual ICollection<Gerecht> Gerechten { get; set; }
        public virtual Klant Klant { get; set; }
    }
}
