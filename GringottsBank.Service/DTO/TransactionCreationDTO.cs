using System;
using System.ComponentModel.DataAnnotations;

namespace GringottsBank.Service.DTO
{
    public class TransactionCreationDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Amount should be there")]
        [Range(0,double.MaxValue,ErrorMessage ="Amount should be greater than 0")]
        public double? Amount { get; set; }
        public DateTime Time { get; set; }
        [MaxLength(100,ErrorMessage ="Maximum 100 characters supported for reference")]
        public string Reference { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Account ID should be there")]
        [Range(10000, double.MaxValue, ErrorMessage = "Amount should be greater than 10000")]
        public int? AccountID { get; set; }
    }
}
