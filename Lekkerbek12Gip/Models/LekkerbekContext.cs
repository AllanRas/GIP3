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
        public DbSet<Chef> Chefs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BestellingGerecht> BestellingGerecht { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bestelling>()
                .HasMany(b => b.Gerechten)
                .WithMany(g => g.Bestellingen)
                .UsingEntity<BestellingGerecht>(
                    bg => bg.HasOne(prop => prop.Gerecht).WithMany().HasForeignKey(prop => prop.GerechtId),
                    bg => bg.HasOne(prop => prop.Bestelling).WithMany().HasForeignKey(prop => prop.BestellingId),
                    bg =>
                    {
                        bg.HasKey(prop => new { prop.BestellingGerechtId});
                        bg.Property(prop => prop.Aantal).HasDefaultValueSql("0");
                    }
              );

            base.OnModelCreating(modelBuilder);
        }
    }
}
