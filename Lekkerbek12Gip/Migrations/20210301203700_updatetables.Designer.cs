﻿// <auto-generated />
using System;
using Lekkerbek12Gip.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lekkerbek12Gip.Migrations
{
    [DbContext(typeof(LekkerbekContext))]
    [Migration("20210301203700_updatetables")]
    partial class updatetables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lekkerbek12Gip.Models.Bestelling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Afgerekend")
                        .HasColumnType("bit");

                    b.Property<DateTime>("AfhaalTijd")
                        .HasColumnType("datetime2");

                    b.Property<int?>("KlantId")
                        .HasColumnType("int");

                    b.Property<string>("SpecialeWensen")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("KlantId");

                    b.ToTable("Bestellings");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Gerecht", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BestellingId")
                        .HasColumnType("int");

                    b.Property<string>("Categorie")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Omschrijving")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Prijs")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BestellingId");

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

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KlantId");

                    b.ToTable("klants");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Bestelling", b =>
                {
                    b.HasOne("Lekkerbek12Gip.Models.Klant", "Klant")
                        .WithMany("Bestellings")
                        .HasForeignKey("KlantId");

                    b.Navigation("Klant");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Gerecht", b =>
                {
                    b.HasOne("Lekkerbek12Gip.Models.Bestelling", null)
                        .WithMany("Gerechten")
                        .HasForeignKey("BestellingId");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Bestelling", b =>
                {
                    b.Navigation("Gerechten");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Klant", b =>
                {
                    b.Navigation("Bestellings");
                });
#pragma warning restore 612, 618
        }
    }
}
