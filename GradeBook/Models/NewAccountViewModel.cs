using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class NewAccountViewModel : AccountViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}