using Lekkerbek12Gip.Models.Product.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Gerecht : IProduct
    {
        public Gerecht()
        {
            this.Bestellingen = new HashSet<Bestelling>();
            if (this.FavKlanten == null)
            {
                this.FavKlanten = new List<GerechtKlantFavoriet>();
            }
        }  
        public int GerechtId { get; set; }
        public int? CategoryId { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Prijs { get; set; }
        //public Categorieen Categorie { get; set; }
        public virtual ICollection<Bestelling> Bestellingen { get; set; }
        public virtual ICollection<GerechtKlantFavoriet> FavKlanten { get; set; }
        public virtual ICollection<Klant> Klanten { get; set; }
        public Category Category { get; set; }

        public decimal WithBtwPrijs => (Prijs * GetBelasting() / 100);

        public int GetBelasting()
        {
            return 6;
        }
    }
}
