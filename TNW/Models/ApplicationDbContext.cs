using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TNW.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}