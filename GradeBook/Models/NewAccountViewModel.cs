using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class NewAccountViewModel : AccountViewModel
    {
        [Required, EmailAddress, StringLength(50)]
        public string Email { get; set; }
    }
}