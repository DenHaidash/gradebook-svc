using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GradeBook.Models.Abstractions;

namespace GradeBook.Models
{
    [Table("Gradebooks")]
    public class Gradebook : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Column(Order = 0)]
        public int SemesterRefId { get; set; }
        
        [Column(Order = 1)]
        public int SubjectRefId { get; set; }
        
        public virtual IEnumerable<Grade> Grades { get; set; }
        
        public virtual IEnumerable<FinalGrade> FinalGrades { get; set; }
        
        public virtual IEnumerable<GradebookTeacher> GradebookTeachers { get; set; }
        
        [ForeignKey("SemesterRefId,SubjectRefId")]
        public virtual SemesterSubject SemesterSubject { get; set; }
        
        [ForeignKey("SemesterRefId")]
        public virtual Semester Semester { get; set; }
        
        [ForeignKey("SubjectRefId")]
        public virtual Subject Subject { get; set; }
    }
}