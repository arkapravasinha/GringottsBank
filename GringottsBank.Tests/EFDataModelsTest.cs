using GringottBank.DataAccess.EF.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace GringottsBank.Tests
{
    [TestClass]
    public class EFDataModelsTest
    {
        [TestMethod]
        public void TestAccount_PropertyCheck()
        {
            //Arrange
            Account account;

            //Act
            account = new Account()
            {
                AccountID = 1,
                AccountNickName = "Test",
                AccountType = AccountType.Savings,
                Balance = 0.0,
                Transactions = new List<Transaction>(),
                Customer = new Customer(),
                CustomerID = 1,
                Nominee ="Test"
            };
            //Assert
            Assert.IsNotNull(account.Customer);
            Assert.IsNotNull(account.Transactions);
            Assert.IsInstanceOfType(account.Transactions, typeof(IList<Transaction>));
            Assert.AreEqual(account.Transactions.Count, 0);
            Assert.AreEqual(account.AccountID, 1);
            Assert.AreEqual(account.AccountNickName, "Test");
            Assert.AreEqual(account.AccountType, AccountType.Savings);
            Assert.AreEqual(account.Balance, 0.0);
            Assert.AreEqual(account.Nominee, "Test");
        }

        [TestMethod]
        public void TestAuditLog_PropertyCheck()
        {
            //Arrange
            AuditLog auditLog;
            var ts = System.DateTime.Now;

            //Act
            auditLog = new AuditLog()
            {
                AffectedColumns = "Test",
                Id = 1,
                NewValues = "Test",
                OldValues = "Test",
                PrimaryKey = "Test",
                TableName = "Test",
                TimeStamp = ts,
                Type = "Test",
                UserId = "Test"
            };

            //assert
            Assert.AreEqual(auditLog.AffectedColumns, "Test");
            Assert.AreEqual(ts, auditLog.TimeStamp);
            Assert.AreEqual(auditLog.OldValues, "Test");
            Assert.AreEqual(auditLog.NewValues, "Test");
            Assert.AreEqual(auditLog.Id, 1);
            Assert.AreEqual(auditLog.PrimaryKey, "Test");
            Assert.AreEqual(auditLog.TableName, "Test");
            Assert.AreEqual(auditLog.Type, "Test");
            Assert.AreEqual(auditLog.UserId, "Test");

        }

        [TestMethod]
        public void TestCustomer_PropertyCheck()
        {
            //Arrange
            Customer customer;

            //Act
            customer = new Customer()
            {
                Accounts = new List<Account>(),
                Address = "Test",
                CustomerId = 1,
                Email = "Test",
                Mobile = "Test",
                Name = "Test",
            };

            //Assert
            Assert.IsNotNull(customer.Accounts);
            Assert.AreEqual(customer.Accounts.Count, 0);
            Assert.AreEqual(customer.Address, "Test"); 
            Assert.AreEqual(customer.CustomerId, 1);
            Assert.AreEqual(customer.Email, "Test");
            Assert.AreEqual(customer.Mobile, "Test");
            Assert.AreEqual(customer.Name, "Test");
        }

        [TestMethod]
        public void TestTransaction_PropertyCheck()
        {
            //Arrange
            Transaction transaction;
            var guid = new System.Guid();
            var ts=System.DateTime.Now;

            //Act
            transaction = new Transaction()
            {
                TransactionID = guid,
                TransactionType = TransactionType.Deposit,
                Account = new Account(),
                AccountID = 1,
                Amount = 0.0,
                Reference = "Test",
                Time = ts,
            };

            //Assert
            Assert.IsNotNull(transaction.Account);
            Assert.AreEqual(transaction.TransactionID, guid);
            Assert.AreEqual(transaction.TransactionType,TransactionType.Deposit);
            Assert.AreEqual(transaction.AccountID,1);
            Assert.AreEqual(transaction.Amount, 0.0);
            Assert.AreEqual(transaction.Reference,"Test"); 
            Assert.AreEqual(transaction.Time, ts);

        }
    }
}
