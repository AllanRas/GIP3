using Lekkerbek12Gip.Controllers;
using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
  
   public class ContrTest
    {
        public readonly Mock<IBestellingsService> _store = new();
        public readonly Mock<IEmailService> _storee = new();
        public readonly Mock<IKlantsService> _storek = new();
        public readonly Mock<IEventsService> _storeEvent = new();


        [Fact]
       async Task test1()
        {
            BestellingsController controller = new BestellingsController(_store.Object, _storee.Object, _storek.Object, _storeEvent.Object);
                Bestelling bestelling = new Bestelling { KlantId=1,AfhaalTijd=DateTime.Now.AddDays(1)};
            var userMock = new Mock<IPrincipal>();
            userMock.Setup(p => p.IsInRole("Admin")).Returns(true);
            userMock.Setup(p => p.IsInRole("Klant")).Returns(false);
            
            _store.Setup(x => x.bestellingCreate(bestelling)).ReturnsAsync(bestelling);
                         
            await controller.Create(bestelling);
             _store.Verify(x=>x.bestellingCreate(bestelling),Times.Once);

          
           
          _store.Verify(x => x.bestellingCreate(bestelling),Times.Never);
          
        }
    }
}
