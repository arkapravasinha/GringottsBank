using GringottBank.DataAccess.EF;
using GringottBank.DataAccess.EF.DataModels;
using GringottBank.DataAccess.Service.Abstractions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GringottBank.DataAccess.Service.Concrete
{
    internal class AccountRepository : GenericRepository<Account>,IAccountRepository
    {
        public AccountRepository(BankDBContext dbContext, ILogger logger):base(dbContext, logger)
        {
        }

        public async Task<Account> FindById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public override async Task<bool> Update(Account entity)
        {
            var oldAccount = await _dbSet.FindAsync(entity.AccountID);
            if (oldAccount == null)
                return false;
            oldAccount.Balance = entity.Balance;
            return true;

        }
    }
}