using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNW.Models;

namespace TNW.Interfaces
{
    public interface IAccountTypeRepository
    {
        IEnumerable<AccountType> GetAll();
        AccountType Get(int id);
        void Update(AccountType accountType);
        void Remove(AccountType record);
        void Add(AccountType account);
    }
}
