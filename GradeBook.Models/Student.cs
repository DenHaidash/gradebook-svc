using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public int AccountRefId { get; set; }
        [ForeignKey("AccountRefId")]
        public virtual Account Account { get; set; }
        public int GroupRefId { get; set; }
        [ForeignKey("GroupRefId")]
        public virtual Group Group { get; set; }
        public virtual IEnumerable<Grade> Grades { get; set; }
        public virtual IEnumerable<FinalGrade> FinalGrades { get; set; }
    }
}