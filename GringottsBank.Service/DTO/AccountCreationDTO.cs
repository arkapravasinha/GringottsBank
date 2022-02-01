using System.ComponentModel.DataAnnotations;

namespace GringottsBank.Service.DTO
{
    public class AccountCreationDTO
    {
        [MaxLength(50, ErrorMessage ="Account nickname must be within 50")]
        public string AccountNickName { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="Account type should be there")]
        [Range(1, 3, ErrorMessage = "Account Type Id should be between 1 to 3")]
        public int? AccountType { get; set; }

        [MaxLength(50, ErrorMessage = "Nominee must be within 50")]
        public string Nominee { get; set; }

        [Required(AllowEmptyStrings = false,ErrorMessage = "Balance should be there")]
        public double? Balance { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Customer ID is required")]
        [Range(100, int.MaxValue, ErrorMessage = "Customer Id should be greater than 100")]
        public int? CustomerID { get; set; }
    }
}
