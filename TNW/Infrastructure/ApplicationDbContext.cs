using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using TNW.Interfaces;
using TNW.Models;

namespace TNW.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<PortfolioAccount> PortfolioAccounts { get; set; }
        public DbSet<AccountValue> AccountValues { get; set; }
        public DbSet<CurrencyType> CurrencyTypes { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PortfolioAccount>()
                .HasRequired(p => p.AccountType)
                .WithMany(a => a.PortfolioAccounts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PortfolioAccount>()
                .HasRequired(p => p.AssetType)
                .WithMany(a => a.PortfolioAccounts)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PortfolioAccount>()
                .HasRequired(p => p.CurrencyType)
                .WithMany(c => c.PortfolioAccounts)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}