using System;

namespace GringottsBank.Service.DTO
{
    public class TransactionResponseDTO
    {
        public Guid TransactionID { get; set; }
        public int TransactionType { get; set; }
        public double Amount { get; set; }
        public DateTime Time { get; set; }
        public string Reference { get; set; }
        public int AccountID { get; set; }
    }
}
