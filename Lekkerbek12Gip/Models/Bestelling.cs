using Lekkerbek12Gip.Models.Product;
using Lekkerbek12Gip.Models.Product.Interface;
using Lekkerbek12Gip.Services.Interfaces;
using Lekkerbek12Gip.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Bestelling
    {
        public Bestelling()
        {
            this.Gerechten = new HashSet<Gerecht>();
            this.Dranken = new HashSet<Drank>();
            if (this.BestellingGerechten == null)
            {
                this.BestellingGerechten = new List<BestellingGerechten>();
            }
            if (this.BestellingDranks == null)
            {
                this.BestellingDranks = new List<BestellingDrank>();
            }
        }
        public enum BestelStatus
        {
            NotReady,
            GettingReady,
            Done
        }

        [Display(Name = "Status")]
        public BestelStatus BestelingStatus { get; set; } = BestelStatus.NotReady;
        [Key]
        public int BestellingId { get; set; }
        [Required(ErrorMessage = "Klant is verplicht")]
        public int? KlantId { get; set; }
        public int? ChefId { get; set; }
        [Display(Name = "Speciale Wensen")]
        public string SpecialeWensen { get; set; }
        [Display(Name = "Order datum")]
        public DateTime OrderDate { get; set; }
        public bool Afgerekend { get; set; }
        [Display(Name = "Afhaal tijd")]
        [BestelDateAttribute]
        public DateTime AfhaalTijd { get; set; }
        public decimal Korting { get; set; }
        [Display(Name = "Totaalprijs")]
        public decimal TotalPrijs
        {
            get
            {
                decimal totalPrijs = 0;
                if (BestellingGerechten != null)
                {

                    totalPrijs += 0;
                    foreach (var item in BestellingGerechten)
                    {
                        totalPrijs += item.Aantal * item.Gerecht.Prijs;
                    }

                    if (Korting == 10)
                    {
                        totalPrijs = totalPrijs * 9 / 10;
                    }
                }
                if (BestellingDranks != null)
                {
                   
                    foreach (var item in BestellingDranks)
                    {
                        totalPrijs += item.Aantal * item.Drank.Prijs;
                    }

                    if (Korting == 10)
                    {
                        totalPrijs = totalPrijs * 9 / 10;
                    }
                }
                return totalPrijs;
            }
        }
        public bool IsConfirmed { get; set; }
        public virtual ICollection<Drank> Dranken { get; set; }
        public virtual ICollection<Gerecht> Gerechten { get; set; }
        public virtual ICollection<BestellingGerechten> BestellingGerechten { get; set; }
        public virtual ICollection<BestellingDrank> BestellingDranks { get; set; }
        [NotMapped]
        public virtual IEnumerable<IProduct> Products { get {
                List<IProduct> products = new List<IProduct>();
                if (Dranken !=null)
                {
                    products.AddRange(Dranken);
                }
                if (Gerechten !=null)
                {
                    products.AddRange(Gerechten);
                }                
                return products;
            } }
        public virtual Klant Klant { get; set; }
        public virtual Chef Chef { get; set; }
    }
}
