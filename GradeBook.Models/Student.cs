using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GradeBook.Models.Abstractions;

namespace GradeBook.Models
{
    [Table("Students")]
    public class Student : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("Id")]
        public virtual Account Account { get; set; }
        
        public int GroupRefId { get; set; }
        
        [ForeignKey("GroupRefId")]
        public virtual Group Group { get; set; }
        
        public virtual IEnumerable<Grade> Grades { get; set; }
        
        public virtual IEnumerable<FinalGrade> FinalGrades { get; set; }
    }
}