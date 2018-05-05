using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    [Table("Specialties")]
    public class Specialty
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(20)]
        public string Code { get; set; }
        
        [Required, MaxLength(50)]
        public string Name { get; set; }
        
        public virtual IEnumerable<Group> Groups { get; set; }
    }
}