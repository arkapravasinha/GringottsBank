using GringottBank.DataAccess.EF;
using GringottBank.DataAccess.EF.DataModels;
using GringottBank.DataAccess.Service.Abstractions;
using Microsoft.Extensions.Logging;

namespace GringottBank.DataAccess.Service.Concrete
{
    internal class AccountRepository : GenericRepository<Account>,IAccountRepository
    {
        public AccountRepository(BankDBContext dbContext, ILogger logger):base(dbContext, logger)
        {
        }
    }
}