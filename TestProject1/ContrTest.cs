using Lekkerbek12Gip.Controllers;
using Lekkerbek12Gip.Models;
using Lekkerbek12Gip.Services.Concrete;
using Lekkerbek12Gip.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
  
    [TestClass]
    public class ContrTest
    {
        [TestMethod]
        public void TestIndexKlant()
        {
            // ARRANGE
            // InMemory db
            var builder = new DbContextOptionsBuilder<LekkerbekContext>();
            builder.UseInMemoryDatabase("LekkerbekGip12");
            LekkerbekContext ctx = new LekkerbekContext(builder.Options);

            //service
            KlantService ks = new KlantService(ctx);
            FirmaService fs = new FirmaService(ctx);

            //Controllers
            KlantsController klantsController = new KlantsController(ctx,ks,fs);

            // ACT
            // test the return of index
            IActionResult result = klantsController.Index().Result;

            // ASSERT
            // test if result is not null
            Assert.IsNotNull(result);
            
            // test if result is a view (html)
            Assert.IsTrue(result is ViewResult);
            
            //Add result into view, check if Model from view is IEnumerable klant
            ViewResult viewResult = (ViewResult)result;
            Assert.IsTrue(viewResult.Model is IEnumerable<Klant>);
        }

        [TestMethod]
        public void TestCreateKlant()
        {

            // ARRANGE
            // InMemory db
            var builder = new DbContextOptionsBuilder<LekkerbekContext>();
            builder.UseInMemoryDatabase("LekkerbekGip12");
            LekkerbekContext ctx = new LekkerbekContext(builder.Options);

            //service
            KlantService ks = new KlantService(ctx);
            FirmaService fs = new FirmaService(ctx);


            //Controllers
            KlantsController klantsController = new KlantsController(ctx, ks, fs);

            // objects setup
            Klant klant = new Klant
            {
                Name = "TestNaam",
                emailadres = "Test@hotmail.com",
                Adress = "TestAdres"
            };
        }

        [TestMethod]
        public void Testtest()
        {

        }


    }
}
