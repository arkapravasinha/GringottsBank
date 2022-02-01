using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottBank.DataAccess.Service.Abstractions
{
    public interface IGringottBankUnitOfWork:IDisposable
    {
        IAccountRepository AccountRepository { get; }

        ICustomerRepository CustomerRepository { get; }

        ITransactionRepository TransactionRepository { get; }

        DatabaseFacade Database { get; }

        Task SaveChangesAsync(string userId);

    }
}
