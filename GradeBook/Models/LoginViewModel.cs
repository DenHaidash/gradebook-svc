using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}