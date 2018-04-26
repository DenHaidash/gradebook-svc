using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    public class Gradebook
    {
        [Key]
        public int Id { get; set; }
        public int GroupRefId { get; set; }
        [ForeignKey("GroupRefId")]
        public virtual Group Group { get; set; }
        public int SubjectRefId { get; set; }
        [ForeignKey("SubjectRefId")]
        public virtual Subject Subject { get; set; }
        public virtual IEnumerable<Grade> Grades { get; set; }
        public virtual IEnumerable<FinalGrade> FinalGrades { get; set; }
        public virtual IEnumerable<GradebookTeacher> GradebookTeachers { get; set; }
    }
}