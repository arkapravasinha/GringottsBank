using GringottBank.DataAccess.EF.DataModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GringottsBank.BusinessLogic.Service
{
    public interface IAccountService
    {
        Task<Guid> AccounDeposit(Transaction transaction, string userId = "testUserId");
        Task<Guid> AccountWithdrwal(Transaction transaction, string userId = "testUserId");
        Task<int> CreateAccount(Account account, string userId = "testUserId");
        Task<Account> GetAccountById(int accountId);
        Task<List<Account>> GetAccountsByCustomerId(int customerId);
        Task<List<Account>> GetAllAccounts();
        Task<List<Transaction>> GetTransactionsByAccountId(int accountId);
    }
}