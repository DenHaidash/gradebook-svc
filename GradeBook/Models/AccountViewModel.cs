using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class AccountViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleName { get; set; }
    }
}