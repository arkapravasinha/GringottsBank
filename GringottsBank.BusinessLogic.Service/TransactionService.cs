using GringottBank.DataAccess.EF.DataModels;
using GringottBank.DataAccess.Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottsBank.BusinessLogic.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IGringottBankUnitOfWork _gringottBankUnitOfWork;

        public TransactionService(IGringottBankUnitOfWork gringottBankUnitOfWork)
        {
            _gringottBankUnitOfWork = gringottBankUnitOfWork;
        }

        public async Task<Transaction> GetTransactionById(Guid? id)
        {
            if (id == null || id == Guid.Empty)
                throw new ArgumentNullException("Transaction id can not be null or empty");
            var transactions = await _gringottBankUnitOfWork.TransactionRepository.Find(x => x.TransactionID == id);
            var transaction = transactions.FirstOrDefault();
            if (transaction == null)
                throw new Exception("Transaction can not be Found");
            return transaction;
        }

        public async Task<List<Transaction>> GetAllTransactionByTimePeriod(int customerId,DateTime start,DateTime end)
        {
            if (customerId < 100)
                throw new ArgumentOutOfRangeException("Customer id can not be less than 100");
            var transactions = await _gringottBankUnitOfWork.TransactionRepository.Find(x =>
                              x.Account.Customer.CustomerId == customerId
                              && x.Time > start && x.Time < end);
            if (transactions == null)
                throw new Exception("Transactions can not be found");
            return transactions.ToList();
        }
    }
}
