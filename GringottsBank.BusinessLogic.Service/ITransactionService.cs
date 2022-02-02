using GringottBank.DataAccess.EF.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GringottsBank.BusinessLogic.Service
{
    public interface ITransactionService
    {
        Task<Transaction> GetTransactionById(Guid? id);

        Task<List<Transaction>> GetAllTransactionByTimePeriod(int customerId, DateTime start, DateTime end);
    }
}