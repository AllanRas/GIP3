using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Event
    {
        
        public int EventId { get; set; }      
        public string Title { get; set; }
        public DateTime Start{ get; set; }
        public DateTime End { get; set; }
    }
}
