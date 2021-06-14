using Lekkerbek12Gip.Models.Product.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models.Product
{
    public class Drank : IProduct
    {
        public int DrankId { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public decimal Prijs { get; set; }
        public string Omschrijving { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Bestelling> Bestellingen { get; set; }

        public decimal WithBtwPrijs => Prijs - (Prijs * GetBelasting() / 100);

        public int GetBelasting()
        {
            if (Category != null && Category.Name == "Alcohol")
            {
                return 21;
            }
            return 6;
        }
    }
}
