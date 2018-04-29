using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class SpecialtyViewModel
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}