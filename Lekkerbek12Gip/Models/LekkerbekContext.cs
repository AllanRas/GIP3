using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lekkerbek12Gip.Models
{
    public class LekkerbekContext : IdentityDbContext
    {
        public LekkerbekContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Bestelling> Bestellings { get; set; }
        public DbSet<Klant> Klants { get; set; }
        public DbSet<Gerecht> Gerechten { get; set; }      
        public DbSet<Chef> Chefs { get; set; }     
        public DbSet<BestellingGerechten> BestellingGerechten { get; set; }
        public DbSet<PlanningsModule> PlanningsModules { get; set; } 
        public DbSet<Event> Events { get; set; }
        public DbSet<ChefPlanningsModule> ChefPlanningsModules { get; set; }
        public DbSet<Firma> Firmas { get; set; }
        public DbSet<GerechtKlantFavoriet> GerechtKlantFavorieten { get; set; }
        public DbSet<Review> Reviews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Bestelling>()
                .HasMany<Gerecht>(b => b.Gerechten)
                .WithMany(g => g.Bestellingen)
                .UsingEntity<BestellingGerechten>(
                    bg => bg.HasOne(prop => prop.Gerecht).WithMany().HasForeignKey(prop => prop.GerechtId),
                    bg => bg.HasOne(prop => prop.Bestelling).WithMany().HasForeignKey(prop => prop.BestellingId),
                    bg =>
                    {
                        bg.HasKey(prop => new { prop.BestellingId, prop.GerechtId});
                        bg.Property(prop => prop.Aantal).HasDefaultValueSql("0");
                    }
              );
            modelBuilder.Entity<PlanningsModule>()
               .HasMany<Chef>(b => b.chefs)
               .WithMany(g => g.Plannings)
               .UsingEntity<ChefPlanningsModule>(
                   bg => bg.HasOne(prop => prop.Chef).WithMany().HasForeignKey(prop => prop.ChefId),
                   bg => bg.HasOne(prop => prop.PlanningsModule).WithMany().HasForeignKey(prop => prop.PlanningsModuleId),
                   bg =>
                   {
                       bg.HasKey(prop => new { prop.PlanningsModuleId, prop.ChefId });
                       bg.Property(prop => prop.ChefStatu);
                   });

            modelBuilder.Entity<Gerecht>()
                .HasMany<Klant>(b => b.Klanten)
                .WithMany(g => g.Fav)
                .UsingEntity<GerechtKlantFavoriet>(
                    bg => bg.HasOne(prop => prop.Klant).WithMany().HasForeignKey(prop => prop.KlantId),
                    bg => bg.HasOne(prop => prop.Gerecht).WithMany().HasForeignKey(prop => prop.GerechtId),
                    bg =>
                    {
                        bg.HasKey(prop => new { prop.GerechtId, prop.KlantId });
                    }
              );

            base.OnModelCreating(modelBuilder);
        }
    }
}
