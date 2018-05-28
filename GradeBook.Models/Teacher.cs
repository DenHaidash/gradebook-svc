using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GradeBook.Models.Abstractions;

namespace GradeBook.Models
{
    [Table("Teachers")]
    public class Teacher : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Id")]
        public virtual Account Account { get; set; }
        
        public virtual IEnumerable<GradebookTeacher> GradebookTeachers { get; set; }
        
        public virtual IEnumerable<FinalGrade> FinalGrades { get; set; }
        
        public virtual IEnumerable<Grade> Grades { get; set; }
    }
}