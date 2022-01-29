using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottBank.DataAccess.EF.DataModels
{
    public  class Account
    {
        public int AccountID { get; set; }
        public string AccountNickName { get; set; }
        public AccountType AccountType { get; set; }
        public string Nominee { get; set; } 
        public double Balance { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public Customer Customer { get; set; }
        public int CustomerID { get; set; }
    }

    public enum AccountType:int
    {
        Savings=1,
        Current,
        Business
    }
}
