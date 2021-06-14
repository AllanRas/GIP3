using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int KlantId { get; set; }
        // Message of review 
        public string ReviewMessage { get; set; }
        // Score 1-5 stars
        public int Score { get; set; }
        public DateTime TimeOfReview { get; set; }
        public virtual Klant Klant { get; set; }
    }
}
