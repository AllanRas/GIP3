using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.ViewModel
{
    public class BesteldeGerechtenIndexModel
    {
      
        public List<BestellingGerechten> BestellingGerechten { get; set; }
        public List<BestellingDrank> BestellingDranks{ get; set; }
        public List<Gerecht> Gerechts{ get; set; }
        public List<Drank> Dranken{ get; set; }
        public List<Gerecht> favGerechten { get; set; }
        public List<BestellingGerechten> ToegevoegdGerechtens { get; set; }
        public List<BestellingDrank> ToegevoegdDranken { get; set; }
        public List<Category> Categories { get; set; }
        public int AantalmatcherGerecht(int gerechtId,int bestellingId)
        {
            var aantal= BestellingGerechten.Where(x => (x.GerechtId == gerechtId) && (x.BestellingId == bestellingId)).FirstOrDefault();
           return aantal==null?0:aantal.Aantal<0?0:aantal.Aantal;
        }
        public int AantalmatcherDrank(int drankId, int bestellingId)
        {
            var aantal = BestellingDranks.Where(x => (x.DrankId == drankId) && (x.BestellingId == bestellingId)).FirstOrDefault();
            return aantal == null ? 0 : aantal.Aantal < 0 ? 0 : aantal.Aantal;
        }
    }
}
