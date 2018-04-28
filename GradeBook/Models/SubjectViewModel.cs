using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class SubjectViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}