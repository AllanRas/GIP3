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
    [Migration("20210426133852_fav")]
    partial class fav
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<int>("BestelingStatus")
                        .HasColumnType("int");

                    b.Property<int?>("ChefId")
                        .HasColumnType("int");

                    b.Property<int?>("KlantId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<decimal>("Korting")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PlanningsModuleId")
                        .HasColumnType("int");

                    b.Property<int?>("SpecialeWensen")
                        .HasColumnType("int");

                    b.HasKey("BestellingId");

                    b.HasIndex("ChefId");

                    b.HasIndex("KlantId");

                    b.HasIndex("PlanningsModuleId");

                    b.ToTable("Bestellings");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.BestellingGerechten", b =>
                {
                    b.Property<int?>("BestellingId")
                        .HasColumnType("int");

                    b.Property<int?>("GerechtId")
                        .HasColumnType("int");

                    b.Property<int>("Aantal")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("0");

                    b.Property<int?>("BestellingId1")
                        .HasColumnType("int");

                    b.HasKey("BestellingId", "GerechtId");

                    b.HasIndex("BestellingId1");

                    b.HasIndex("GerechtId");

                    b.ToTable("BestellingGerechten");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Chef", b =>
                {
                    b.Property<int>("ChefId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChefName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChefId");

                    b.ToTable("Chefs");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.ChefPlanningsModule", b =>
                {
                    b.Property<int?>("PlanningsModuleId")
                        .HasColumnType("int");

                    b.Property<int?>("ChefId")
                        .HasColumnType("int");

                    b.Property<int>("ChefStatu")
                        .HasColumnType("int");

                    b.Property<int?>("PlanningsModuleId1")
                        .HasColumnType("int");

                    b.HasKey("PlanningsModuleId", "ChefId");

                    b.HasIndex("ChefId");

                    b.HasIndex("PlanningsModuleId1");

                    b.ToTable("ChefPlanningsModules");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PlanningsModuleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EventId");

                    b.HasIndex("PlanningsModuleId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Firma", b =>
                {
                    b.Property<int>("FirmaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BtwNummer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirmaNaam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("KlantId")
                        .HasColumnType("int");

                    b.HasKey("FirmaId");

                    b.HasIndex("KlantId")
                        .IsUnique()
                        .HasFilter("[KlantId] IS NOT NULL");

                    b.ToTable("Firmas");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Gerecht", b =>
                {
                    b.Property<int>("GerechtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Categorie")
                        .HasColumnType("int");

                    b.Property<int?>("KlantId")
                        .HasColumnType("int");

                    b.Property<string>("Naam")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Omschrijving")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Prijs")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("GerechtId");

                    b.HasIndex("KlantId");

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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("emailadres")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KlantId");

                    b.ToTable("Klants");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.PlanningsModule", b =>
                {
                    b.Property<int>("PlanningsModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChefId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OpeningsUren")
                        .HasColumnType("datetime2");

                    b.HasKey("PlanningsModuleId");

                    b.ToTable("PlanningsModules");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Bestelling", b =>
                {
                    b.HasOne("Lekkerbek12Gip.Models.Chef", "Chef")
                        .WithMany("Bestellings")
                        .HasForeignKey("ChefId");

                    b.HasOne("Lekkerbek12Gip.Models.Klant", "Klant")
                        .WithMany("Bestellings")
                        .HasForeignKey("KlantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lekkerbek12Gip.Models.PlanningsModule", null)
                        .WithMany("Bestellings")
                        .HasForeignKey("PlanningsModuleId");

                    b.Navigation("Chef");

                    b.Navigation("Klant");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.BestellingGerechten", b =>
                {
                    b.HasOne("Lekkerbek12Gip.Models.Bestelling", "Bestelling")
                        .WithMany()
                        .HasForeignKey("BestellingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lekkerbek12Gip.Models.Bestelling", null)
                        .WithMany("BestellingGerechten")
                        .HasForeignKey("BestellingId1");

                    b.HasOne("Lekkerbek12Gip.Models.Gerecht", "Gerecht")
                        .WithMany()
                        .HasForeignKey("GerechtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bestelling");

                    b.Navigation("Gerecht");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.ChefPlanningsModule", b =>
                {
                    b.HasOne("Lekkerbek12Gip.Models.Chef", "Chef")
                        .WithMany()
                        .HasForeignKey("ChefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lekkerbek12Gip.Models.PlanningsModule", "PlanningsModule")
                        .WithMany()
                        .HasForeignKey("PlanningsModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lekkerbek12Gip.Models.PlanningsModule", null)
                        .WithMany("ChefPlanningsModules")
                        .HasForeignKey("PlanningsModuleId1");

                    b.Navigation("Chef");

                    b.Navigation("PlanningsModule");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Event", b =>
                {
                    b.HasOne("Lekkerbek12Gip.Models.PlanningsModule", null)
                        .WithMany("Events")
                        .HasForeignKey("PlanningsModuleId");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Firma", b =>
                {
                    b.HasOne("Lekkerbek12Gip.Models.Klant", "Klant")
                        .WithOne("Firma")
                        .HasForeignKey("Lekkerbek12Gip.Models.Firma", "KlantId");

                    b.Navigation("Klant");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Gerecht", b =>
                {
                    b.HasOne("Lekkerbek12Gip.Models.Klant", null)
                        .WithMany("Fav")
                        .HasForeignKey("KlantId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Bestelling", b =>
                {
                    b.Navigation("BestellingGerechten");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Chef", b =>
                {
                    b.Navigation("Bestellings");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.Klant", b =>
                {
                    b.Navigation("Bestellings");

                    b.Navigation("Fav");

                    b.Navigation("Firma");
                });

            modelBuilder.Entity("Lekkerbek12Gip.Models.PlanningsModule", b =>
                {
                    b.Navigation("Bestellings");

                    b.Navigation("ChefPlanningsModules");

                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
