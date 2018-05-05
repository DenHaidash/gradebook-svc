using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class AccountViewModel
    {
        [Required, StringLength(20)]
        public string FirstName { get; set; }
        
        [Required, StringLength(20)]
        public string LastName { get; set; }
        
        [Required, StringLength(20)]
        public string MiddleName { get; set; }
    }
}