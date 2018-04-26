using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class AssestmentType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}