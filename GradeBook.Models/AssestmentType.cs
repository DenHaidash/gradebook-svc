using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    [Table("AssestmentTypes")]
    public class AssestmentType
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(20)]
        public string Description { get; set; }
        
        public virtual IEnumerable<SemesterSubject> Semesters { get; set; }
    }
}