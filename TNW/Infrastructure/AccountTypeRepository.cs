using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNW.Interfaces;
using TNW.Models;
using System.Data;
using System.Data.Entity;

namespace TNW.Infrastructure
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private ApplicationDbContext _context;

        public AccountTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(AccountType account)
        {
            _context.AccountTypes.Add(account);
        }

        public AccountType Get(int id)
        {
            return _context.AccountTypes.Where(a => a.Id == id).SingleOrDefault();
        }

        public IEnumerable<AccountType> GetAll()
        {
            return _context
                .AccountTypes
                .OrderBy(a => a.Name)
                .ToList();
        }

        public void Remove(AccountType record)
        {
            _context.AccountTypes.Remove(record);
        }

        public void Update(AccountType accountType)
        {
            _context.Entry(accountType).State = EntityState.Modified;
        }
    }
}
