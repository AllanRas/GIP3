using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models.Mails
{
    public class BevestigMail : IEmail
    {
        public string Message
        {
            get
            {



                string message = string.Empty;
                decimal totalprijs = 0;
                var korting = "";
                foreach (var item in Bestellings)
                {
                    message += $"<li>{item.Gerecht.Naam} \t {item.Gerecht.Prijs}€ - {item.Aantal} stuk</li>";

                    totalprijs += item.Gerecht.Prijs * item.Aantal;

                }
                if (Bestellings.Count > 0 && Bestellings.First().Bestelling.Korting == 10)
                {
                    korting = (totalprijs * 1 / 10).ToString();
                    totalprijs = totalprijs * 9 / 10;
                }
                string template =
                   $@"
                        <html>                      
                        <body>
                        <div width:150px style='border:1px solid black;margin:auto;padding:20px'>
                        <h2 ; style=' ;
                         text-decoration: underline;
                         text-align: center;                       
                                                                             
                        '>Lekkerbek Restaurant!</h2>
                        <ol>{message}
                        </ol>
                        <div>Total prijs:{totalprijs}€</div>
                        <div>korting:{korting}€</div>
                        <p>Uw bestelling is bevestigd</p> 
                        </div>
                        </body>
                        </html>";
                return template;
            }
        }

        public List<BestellingGerechten> Bestellings { get; set; }
    }
}
