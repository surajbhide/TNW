using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNW.Models;

namespace TNW.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<AccountType> AccountTypes { get; }
        IGenericRepository<AssetType> AssetTypes { get; }
        IGenericRepository<CurrencyType> CurrencyTypes { get; }
        void Complete();
    }
}
