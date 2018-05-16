using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    [Table("Teachers")]
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Id")]
        public virtual Account Account { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public virtual IEnumerable<GradebookTeacher> GradebookTeachers { get; set; }
        
        public virtual IEnumerable<FinalGrade> FinalGrades { get; set; }
        
        public virtual IEnumerable<Grade> Grades { get; set; }
    }
}