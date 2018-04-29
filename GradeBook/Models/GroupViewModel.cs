using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class GroupViewModel
    {
        [Required]
        public string Code { get; set; }
        public int SpecialtyId { get; set; }
    }
}