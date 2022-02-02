using GringottBank.DataAccess.EF;
using GringottBank.DataAccess.EF.DataModels;
using GringottBank.DataAccess.Service.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottsBank.BusinessLogic.Service
{
    public class AccountService : IAccountService
    {
        private readonly IGringottBankUnitOfWork _gringottBankUnitOfWork;

        public AccountService(IGringottBankUnitOfWork gringottBankUnitOfWork)
        {
            _gringottBankUnitOfWork = gringottBankUnitOfWork;
        }
        public async Task<List<Account>> GetAllAccounts()
        {
            var results = await _gringottBankUnitOfWork.AccountRepository.All();
            return results.ToList();
        }
        public async Task<int> CreateAccount(Account account, string userId = "testUserId")
        {
            if (account == null)
                throw new ArgumentNullException("account can not be null");
            try
            {
                await _gringottBankUnitOfWork.Database.BeginTransactionAsync();
                await _gringottBankUnitOfWork.AccountRepository.Add(account);
                await _gringottBankUnitOfWork.SaveChangesAsync(userId);
                await _gringottBankUnitOfWork.Database.CommitTransactionAsync();
                return account.AccountID;
            }
            catch (Exception)
            {
                await _gringottBankUnitOfWork.Database.RollbackTransactionAsync();
                throw;
            }
        }
        public async Task<Account> GetAccountById(int accountId)
        {
            if (accountId < 10000)
                throw new ArgumentOutOfRangeException("accountId should be greater than 10000");
            var account = await _gringottBankUnitOfWork.AccountRepository.FindById(accountId);
            if (account == null)
                throw new Exception("Account is present");
            return account;
        }
        public async Task<List<Account>> GetAccountsByCustomerId(int customerId)
        {
            if (customerId < 100)
                throw new ArgumentOutOfRangeException("Customer Id should be greater than 100");
            var accounts = await _gringottBankUnitOfWork.AccountRepository.Find(x => x.CustomerID == customerId);
            if (accounts == null)
                throw new Exception("Customer does not have any accounts");
            return accounts.ToList();

        }
        public async Task<List<Transaction>> GetTransactionsByAccountId(int accountId)
        {
            if (accountId < 10000)
                throw new ArgumentOutOfRangeException("accountId should be greater than 10000");
            var transactions = await _gringottBankUnitOfWork.TransactionRepository.Find(x => x.AccountID == accountId);
            if (transactions == null)
                throw new Exception("Accounts does not have any transactions");
            return transactions.ToList();

        }
        public async Task<Guid> AccounDeposit(Transaction transaction, string userId = "testUserId")
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction can not be null");
            if (transaction.AccountID < 10000)
                throw new ArgumentOutOfRangeException("transaction should be greater than 10000");

            var account = await _gringottBankUnitOfWork.AccountRepository.FindById(transaction.AccountID);
            if (account == null)
                throw new Exception("Account can not be found");
            try
            {
                await _gringottBankUnitOfWork.Database.BeginTransactionAsync();
                transaction.Time = DateTime.Now;
                transaction.TransactionType = TransactionType.Deposit;
                await _gringottBankUnitOfWork.TransactionRepository.Add(transaction);
                account.Balance += transaction.Amount;
                await _gringottBankUnitOfWork.SaveChangesAsync(userId);
                await _gringottBankUnitOfWork.Database.CommitTransactionAsync();
                return transaction.TransactionID;
            }
            catch (Exception)
            {
                await _gringottBankUnitOfWork.Database.RollbackTransactionAsync();
                throw;
            }

        }

        public async Task<Guid> AccountWithdrwal(Transaction transaction, string userId = "testUserId")
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction can not be null");
            if (transaction.AccountID < 10000)
                throw new ArgumentOutOfRangeException("transaction should be greater than 10000");

            var account = await _gringottBankUnitOfWork.AccountRepository.FindById(transaction.AccountID);
            if (account == null)
                throw new Exception("Account can not be found");
            if (account.Balance < transaction.Amount)
                throw new Exception("Can't withdraw amount greater than balance");
            try
            {
                await _gringottBankUnitOfWork.Database.BeginTransactionAsync();
                transaction.Time = DateTime.Now;
                transaction.TransactionType = TransactionType.Withdrawal;
                await _gringottBankUnitOfWork.TransactionRepository.Add(transaction);
                account.Balance -= transaction.Amount;
                await _gringottBankUnitOfWork.SaveChangesAsync(userId);
                await _gringottBankUnitOfWork.Database.CommitTransactionAsync();
                return transaction.TransactionID;
            }
            catch (Exception)
            {
                await _gringottBankUnitOfWork.Database.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
