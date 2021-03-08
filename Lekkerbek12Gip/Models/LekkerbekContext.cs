using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class LekkerbekContext : DbContext
    {
        public LekkerbekContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Bestelling> Bestellings { get; set; }
        public DbSet<Klant> Klants { get; set; }
        public DbSet<Gerecht> Gerechten { get; set; }
        public DbSet<BestellingGerecht> bestellingGerechts { get; set; }
        public DbSet<Chef> Chefs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
