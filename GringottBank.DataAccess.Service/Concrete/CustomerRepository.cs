using GringottBank.DataAccess.EF;
using GringottBank.DataAccess.EF.DataModels;
using GringottBank.DataAccess.Service.Abstractions;
using Microsoft.Extensions.Logging;

namespace GringottBank.DataAccess.Service.Concrete
{
    internal class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {

        public CustomerRepository(BankDBContext dbContext, ILogger logger):base(dbContext, logger)
        {
        }


    }
}