using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class SubjectViewModel
    {
        [Required, StringLength(30)]
        public string Name { get; set; }
    }
}