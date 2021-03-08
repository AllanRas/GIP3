﻿// <auto-generated />
using System;
using Lekkerbek12Gip.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lekkerbek12Gip.Migrations
{
    [DbContext(typeof(LekkerbekContext))]
    partial class LekkerbekContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BestellingGerecht", b =>
                {
                    b.Property<int>("BestellingenBestellingId")
                        .HasColumnType("int");

                    b.Property<int>("GerechtenGerechtId")
                        .HasColumnType("int");

                    b.HasKey("BestellingenBestellingId", "GerechtenGerechtId");

                    b.HasIndex("GerechtenGerechtId");

                    b.ToTable("BestellingGerecht");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Bestelling", b =>
                {
                    b.Property<int>("BestellingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Afgerekend")
                        .HasColumnType("bit");

                    b.Property<DateTime>("AfhaalTijd")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ChefId")
                        .HasColumnType("int");

                    b.Property<int?>("KlantId")
                        .HasColumnType("int");

                    b.Property<decimal>("Korting")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SpecialeWensen")
                        .HasColumnType("int");

                    b.HasKey("BestellingId");

                    b.HasIndex("ChefId");

                    b.HasIndex("KlantId");

                    b.ToTable("Bestellings");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Chef", b =>
                {
                    b.Property<int>("ChefId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChefName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChefId");

                    b.ToTable("Chefs");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Gerecht", b =>
                {
                    b.Property<int>("GerechtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Categorie")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Omschrijving")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Prijs")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("GerechtId");

                    b.ToTable("Gerechten");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Klant", b =>
                {
                    b.Property<int>("KlantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Geboortedatum")
                        .HasColumnType("datetime2");

                    b.Property<int>("GetrouwheidsScore")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KlantId");

                    b.ToTable("Klants");
                });

            modelBuilder.Entity("BestellingGerecht", b =>
                {
                    b.HasOne("Lekkerbek12Gip.Models.Bestelling", null)
                        .WithMany()
                        .HasForeignKey("BestellingenBestellingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lekkerbek12Gip.Models.Gerecht", null)
                        .WithMany()
                        .HasForeignKey("GerechtenGerechtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Bestelling", b =>
                {
                    b.HasOne("Lekkerbek12Gip.Models.Chef", "Chef")
                        .WithMany("Bestellings")
                        .HasForeignKey("ChefId");

                    b.HasOne("Lekkerbek12Gip.Models.Klant", "Klant")
                        .WithMany("Bestellings")
                        .HasForeignKey("KlantId");

                    b.Navigation("Chef");

                    b.Navigation("Klant");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Chef", b =>
                {
                    b.Navigation("Bestellings");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Klant", b =>
                {
                    b.Navigation("Bestellings");
                });
#pragma warning restore 612, 618
        }
    }
}
