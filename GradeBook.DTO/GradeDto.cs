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
        public virtual StudentDto Student { get; set; }
        public virtual TeacherDto Teacher { get; set; }
        public virtual GradebookDto Gradebook { get; set; }
    }
}