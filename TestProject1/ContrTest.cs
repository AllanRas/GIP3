using Lekkerbek12Gip.Controllers;
using Lekkerbek12Gip.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
  
   public class ContrTest
    {
        public readonly Mock<IBestellingsService> _store = new();
        public readonly Mock<IEmailService> _storee = new();
        

        [Fact]
        void test1()
        {    

        }
    }
}
