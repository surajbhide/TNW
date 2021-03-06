﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNW.Interfaces;
using TNW.Models;

namespace TNW.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ApplicationDbContext _context;

        public IGenericRepository<AccountType> AccountTypes { get; private set; }
        public IGenericRepository<AssetType> AssetTypes { get; private set; }

        public IGenericRepository<CurrencyType> CurrencyTypes { get; private set; }

        public IGenericRepository<PortfolioAccount> PortfolioAccounts { get; private set; }

        public IGenericRepository<AccountValue> AccountValues { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            AccountTypes = new GenericRepository<AccountType>(_context);
            AssetTypes = new GenericRepository<AssetType>(_context);
            CurrencyTypes = new GenericRepository<CurrencyType>(_context);
            PortfolioAccounts = new GenericRepository<PortfolioAccount>(_context);
            AccountValues = new GenericRepository<AccountValue>(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
