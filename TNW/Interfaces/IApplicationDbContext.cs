using System.Data.Entity;
using TNW.Models;

namespace TNW.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<AccountType> AccountTypes { get; set; }
        DbSet<AccountValue> AccountValues { get; set; }
        DbSet<AssetType> AssetTypes { get; set; }
        DbSet<CurrencyType> CurrencyTypes { get; set; }
        DbSet<PortfolioAccount> PortfolioAccounts { get; set; }
    }
}