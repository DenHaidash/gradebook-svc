using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    public class Teacher
    {
        [Key]
        public int Id { get; set; }
        public int AccountRefId { get; set; }
        [ForeignKey("AccountRefId")]
        public virtual Account Account { get; set; }
        public virtual IEnumerable<TeacherSubject> Specializations { get; set; }
        public virtual IEnumerable<GradebookTeacher> GradebookTeachers { get; set; }
    }
}