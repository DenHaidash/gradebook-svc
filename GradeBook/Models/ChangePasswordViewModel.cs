using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}