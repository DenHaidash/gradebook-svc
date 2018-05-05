using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    [Table("Grades")]
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        
        public int Value { get; set; }
        
        [Required, MaxLength(50)]
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public int GradebookRefId { get; set; }
        
        [ForeignKey("GradebookRefId")]
        public virtual Gradebook Gradebook { get; set; }
        
        public int StudentRefId { get; set; }
        
        [ForeignKey("StudentRefId")]
        public virtual Student Student { get; set; }
        
        public int TeacherRefId { get; set; }
        
        [ForeignKey("TeacherRefId")]
        public virtual Teacher Teacher { get; set; }
    }
}