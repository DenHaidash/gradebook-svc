using System;

namespace GradeBook.DTO
{
    public class GradeDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public int GradebookRefId { get; set; }
        public StudentDto Student { get; set; }
        public TeacherDto Teacher { get; set; }
        public GradebookDto Gradebook { get; set; }
    }
}