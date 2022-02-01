using GringottBank.DataAccess.EF;
using GringottBank.DataAccess.Service.Abstractions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottBank.DataAccess.Service.Concrete
{
    public class GringottBankUnitOfWork : IGringottBankUnitOfWork
    {
        private readonly BankDBContext _dbContext;
        public GringottBankUnitOfWork(BankDBContext dbContext,ILogger<GringottBankUnitOfWork> logger)
        {
            _dbContext = dbContext;
            AccountRepository= new AccountRepository(_dbContext, logger);
            CustomerRepository= new CustomerRepository(_dbContext, logger);
            TransactionRepository = new TransactionRepository(_dbContext, logger);
        }
        public IAccountRepository AccountRepository { get; private set; }

        public ICustomerRepository CustomerRepository { get; private set; } 

        public ITransactionRepository TransactionRepository { get; private set; } 

        public DatabaseFacade Database => _dbContext.Database;

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task SaveChangesAsync(string userId)
        {
            await _dbContext.SaveChangesAsync(userId);
        }
    }
}
