using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual IEnumerable<SemesterSchuduleSubject> Semesters { get; set; }
    }
}