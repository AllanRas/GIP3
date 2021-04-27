using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Gerecht
    {
        public Gerecht()
        {
            this.Bestellingen = new HashSet<Bestelling>();
            if (this.FavKlanten == null)
            {
                this.FavKlanten = new List<GerechtKlantFavoriet>();
            }
        }

        public enum Categorieen
        {
            Sandwiches = 0,
            Salades = 1,
            Vleesgerechten = 2,
            Visgerechten = 3,
            Frisdranken = 4,
            BierenVat = 5,
            BierenFles = 6,
            Trappist = 7,
            Huiswijnen = 8,
            WijnenSugWit = 9,
            WijnenSugRood = 10
        }
        public int GerechtId { get; set; }
        public string Naam { get; set; }
        public string Omschrijving { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Prijs { get; set; }
        public Categorieen Categorie { get; set; }
        public virtual ICollection<Bestelling> Bestellingen { get; set; }
        public virtual ICollection<GerechtKlantFavoriet> FavKlanten { get; set; }
        public virtual ICollection<Klant> Klanten { get; set; }
    }
}
