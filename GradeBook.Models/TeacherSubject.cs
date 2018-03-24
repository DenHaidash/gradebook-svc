using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    public class TeacherSubject
    {
        public int SubjectRefId { get; set; }
        [ForeignKey("SubjectRefId")]
        public Subject Subject { get; set; }
        public int TeacherRefId { get; set; }
        [ForeignKey("TeacherRefId")]
        public Teacher Teacher { get; set; }
    }
}