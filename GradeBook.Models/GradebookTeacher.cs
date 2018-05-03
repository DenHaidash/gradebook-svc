using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    public class GradebookTeacher
    {
        public int GradebookRefId { get; set; }
        [ForeignKey("GradebookRefId")]
        public Gradebook Gradebook { get; set; }
        public int TeacherRefId { get; set; }
        [ForeignKey("TeacherRefId")]
        public Teacher Teacher { get; set; }
    }
}