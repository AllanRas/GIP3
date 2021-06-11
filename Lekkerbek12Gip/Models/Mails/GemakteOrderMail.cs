using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models.Mails
{
    public class GemakteOrderMail : IEmail
    {
        public string Message { get {
                var message = "";
                decimal totalPrijs = 0;
                foreach (var item in Bestellings)
                {
                    message += $"<div>{item.Gerecht.Naam} \t {item.Gerecht.Prijs}-{item.Aantal}stuk</div>";
                    totalPrijs += item.Gerecht.Prijs * item.Aantal;
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
                        <div></div>
                        <p>Uw bestelling wacht op bevestiging.</p> 
                        </div>
                        </body>
                        </html>";
                return template;                                
            } }
        public List<BestellingGerechten> Bestellings { get ; set ; }
    }
}
