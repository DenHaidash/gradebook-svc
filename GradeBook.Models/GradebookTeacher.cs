using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GradeBook.Models.Abstractions;

namespace GradeBook.Models
{
    [Table("GradebooksTeachers")]
    public class GradebookTeacher : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        public int GradebookRefId { get; set; }
        
        [ForeignKey("GradebookRefId")]
        public Gradebook Gradebook { get; set; }
        
        public int TeacherRefId { get; set; }
        
        [ForeignKey("TeacherRefId")]
        public Teacher Teacher { get; set; }
    }
}