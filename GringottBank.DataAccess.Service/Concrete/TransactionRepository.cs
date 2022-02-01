using GringottBank.DataAccess.EF;
using GringottBank.DataAccess.EF.DataModels;
using GringottBank.DataAccess.Service.Abstractions;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GringottBank.DataAccess.Service.Concrete
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {

        public TransactionRepository(BankDBContext dbContext, ILogger logger):base(dbContext,logger)
        {
        }
    }
}