using System.ComponentModel.DataAnnotations;

namespace GringottsBank.Service.DTO
{
    public class CustomerCreationDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name should be there")]
        [MaxLength(50,ErrorMessage ="Name should be 50 chars")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Email  should be there")]
        [EmailAddress(ErrorMessage = "Valid Email  should be there")]
        [MaxLength(50, ErrorMessage = "Email should be 50 chars")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Address  should be there")]
        [MaxLength(256, ErrorMessage = "Addres should be 256 chars")]
        public string Address { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone should be there")]
        [Phone(ErrorMessage ="Valid Phone No required")]
        public string Mobile { get; set; }
    }
}
