using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GringottBank.DataAccess.EF.DataModels
{
    public class Transaction
    {
        public Guid TransactionID { get; set; }
        public TransactionType TransactionType { get; set; }
        public double Amount { get; set; }
        public DateTime Time { get; set; }
        public string Reference { get; set; }
        public Account Account { get; set; }
        public int AccountID { get; set; }
    }

    public enum TransactionType:int
    {
        Withdrawal=1,
        Deposit
    }
}
