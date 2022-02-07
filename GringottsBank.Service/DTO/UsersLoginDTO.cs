using System.ComponentModel.DataAnnotations;

namespace GringottsBank.Service.DTO
{
    public class UsersLoginDTO
    {
        [Required]
        public string UserName
        {
            get;
            set;
        }
        [Required]
        public string Password
        {
            get;
            set;
        }
        public UsersLoginDTO() { }
    }
}
