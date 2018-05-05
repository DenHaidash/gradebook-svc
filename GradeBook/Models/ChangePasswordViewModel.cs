using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class ChangePasswordViewModel
    {
        [Required, StringLength(20)]
        public string OldPassword { get; set; }
        
        [Required, StringLength(20), MinLength(6)]
        public string NewPassword { get; set; }
    }
}