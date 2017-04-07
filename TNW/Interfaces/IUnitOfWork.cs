using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TNW.Interfaces
{
    public interface IUnitOfWork
    {
        IAccountTypeRepository AccountTypes { get; }
        void Complete();
    }
}
