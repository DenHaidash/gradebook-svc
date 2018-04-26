using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Id")]
        public virtual Account Account { get; set; }
        public bool IsDeleted { get; set; }
        public int GroupRefId { get; set; }
        [ForeignKey("GroupRefId")]
        public virtual Group Group { get; set; }
        public virtual IEnumerable<Grade> Grades { get; set; }
        public virtual IEnumerable<FinalGrade> FinalGrades { get; set; }
    }
}