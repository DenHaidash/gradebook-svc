using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    public class Curriculum
    {
        [Key]
        public int Id { get; set; }
        public int SpecialtyRefId { get; set; }
        [ForeignKey("SpecialtyRefId")]
        public virtual Specialty Specialty { get; set; }
        public int SemesterRefId { get; set; }
        [ForeignKey("SemesterRefId")]
        public virtual Semester Semester { get; set; }
        public virtual IEnumerable<CurriculumSubject> CurriculumSubjects { get; set; }
    }
}