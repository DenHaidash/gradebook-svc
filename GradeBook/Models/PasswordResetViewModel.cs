using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class PasswordResetViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}