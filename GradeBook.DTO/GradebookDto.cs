using System.Collections.Generic;

namespace GradeBook.DTO
{
    public class GradebookDto
    {
        public int Id { get; set; }
        public virtual GroupDto Group { get; set; }
        public virtual SubjectDto Subject { get; set; }
        public virtual IEnumerable<GradeDto> Grades { get; set; }
        public virtual IEnumerable<GradeDto> FinalGrades { get; set; }
    }
}