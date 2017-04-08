using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Cobra.Data;
using Cobra.Models;

namespace Cobra.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cobra.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Cobra.Models.FactorForPaperBag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("CoatedArtPaper");

                    b.Property<DateTime>("CreateTime");

                    b.Property<float>("FoilPaper");

                    b.Property<float>("Glitter");

                    b.Property<float>("Glossy");

                    b.Property<float>("GrosgrainRibbon");

                    b.Property<float>("HotStamping");

                    b.Property<float>("LaborOfFolding");

                    b.Property<float>("LaborOfLargeSize");

                    b.Property<float>("LaborOfMediumSize");

                    b.Property<float>("LaborOfPrintOnePaper");

                    b.Property<float>("LaborOfSmallSize");

                    b.Property<float>("Matt");

                    b.Property<float>("MetalisedPaper");

                    b.Property<float>("NaturalKraftPaper");

                    b.Property<float>("OrganzaRibbon");

                    b.Property<float>("OtherEmbellishment");

                    b.Property<float>("OtherHandle");

                    b.Property<float>("PP");

                    b.Property<float>("PaperTwist");

                    b.Property<float>("PriceOfJHook");

                    b.Property<float>("PriceOfLabel");

                    b.Property<float>("PriceOfTag");

                    b.Property<float>("SantinRibbon");

                    b.Property<float>("SpotUV");

                    b.Property<float>("TipOn");

                    b.Property<float>("WhiteCardboard");

                    b.Property<float>("WhiteKraftPaper");

                    b.HasKey("Id");

                    b.ToTable("FactorForPaperBag");
                });

            modelBuilder.Entity("Cobra.Models.PaperBag", b =>
                {
                    b.Property<int>("Id");

                    b.Property<float?>("ConfirmedPrice");

                    b.Property<string>("Description");

                    b.Property<float?>("EvaluatedPrice");

                    b.Property<float>("Gusset");

                    b.Property<int>("Handle");

                    b.Property<bool>("HasJHook");

                    b.Property<bool>("HasLabel");

                    b.Property<bool>("HasTag");

                    b.Property<float>("Height");

                    b.Property<int>("InnerPackingWay");

                    b.Property<int>("InsidePrinting");

                    b.Property<int>("Lamination");

                    b.Property<int>("Material");

                    b.Property<int>("MaterialWeight");

                    b.Property<int>("OuterPackingWay");

                    b.Property<int>("PreOrderId");

                    b.Property<int>("Printing");

                    b.Property<int>("Quantity");

                    b.Property<int>("ShippingWay");

                    b.Property<int>("SurfaceEmbellishment");

                    b.Property<int>("Unit");

                    b.Property<float>("Width");

                    b.HasKey("Id");

                    b.HasIndex("PreOrderId");

                    b.ToTable("PaperBag");
                });

            modelBuilder.Entity("Cobra.Models.PaperBox", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Brand");

                    b.Property<float?>("ConfirmedPrice");

                    b.Property<string>("Description");

                    b.Property<float?>("EvaluatedPrice");

                    b.Property<float>("Height");

                    b.Property<float>("Length");

                    b.Property<int>("NumberOfDesign");

                    b.Property<int>("PreOrderId");

                    b.Property<int>("PrintingStyle");

                    b.Property<int>("Quantity");

                    b.Property<int>("Unit");

                    b.Property<float>("Width");

                    b.HasKey("Id");

                    b.HasIndex("PreOrderId");

                    b.ToTable("PaperBox");
                });

            modelBuilder.Entity("Cobra.Models.PreOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("MappedFiles");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<int>("Status");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdateTime");

                    b.Property<string>("UpdateUserId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UpdateUserId");

                    b.HasIndex("UserId");

                    b.ToTable("PreOrder");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Cobra.Models.PaperBag", b =>
                {
                    b.HasOne("Cobra.Models.PreOrder", "PreOrder")
                        .WithMany("PaperBags")
                        .HasForeignKey("PreOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Cobra.Models.PaperBox", b =>
                {
                    b.HasOne("Cobra.Models.PreOrder", "PreOrder")
                        .WithMany("PaperBoxs")
                        .HasForeignKey("PreOrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Cobra.Models.PreOrder", b =>
                {
                    b.HasOne("Cobra.Models.ApplicationUser", "UpdateUser")
                        .WithMany()
                        .HasForeignKey("UpdateUserId");

                    b.HasOne("Cobra.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Cobra.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Cobra.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Cobra.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
