using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class Semester
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public int CourseNumber { get; set; }
        public int CourseSemesterNumber { get; set; }
    }
}