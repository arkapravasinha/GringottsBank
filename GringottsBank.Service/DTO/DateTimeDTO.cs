using System;
using System.ComponentModel.DataAnnotations;

namespace GringottsBank.Service.DTO
{
    public class DateTimeDTO
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Start Time is required")]
        public DateTime StartTime { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "End Time is required")]
        public DateTime EndTime { get; set; }

    }
}
