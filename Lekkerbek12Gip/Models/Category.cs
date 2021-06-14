using Lekkerbek12Gip.Models.Product.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        //public virtual ICollection<Gerecht> Gerechten { get; set; }
        public List<String> products { get {
                List<string> names = new List<string>();
                foreach (Type mytype in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                  .Where(mytype => mytype.GetInterfaces().Contains(typeof(IProduct))))
                {
                    names.Add(mytype.Name);
                    Console.WriteLine( mytype.Name ); 
                    //do stuff
                }
                return names;
            } }

    }
}
