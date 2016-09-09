using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cobra.Models;

namespace Cobra.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<PaperBag>()
                .HasOne(p => p.PreOrder)
                .WithMany(b => b.PaperBags);

            builder.Entity<PaperBox>()
                .HasOne(p => p.PreOrder)
                .WithMany(b => b.PaperBoxs);
          
        }

        public DbSet<PreOrder> PreOrder { get; set; }
        public DbSet<PaperBox> PaperBox { get; set; }
        public DbSet<PaperBag> PaperBag { get; set; }

        public DbSet<FactorForPaperBag> FactorForPaperBag { get; set; }
    }
}
