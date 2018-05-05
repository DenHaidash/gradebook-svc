using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class PasswordResetViewModel
    {
        [Required, EmailAddress, StringLength(50)]
        public string Email { get; set; }
    }
}