using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    [Table("Subjects")]
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(30)]
        public string Name { get; set; }
        
        public virtual IEnumerable<SemesterSubject> Semesters { get; set; }
    }
}