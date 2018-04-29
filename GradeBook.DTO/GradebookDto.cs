using System.Collections.Generic;

namespace GradeBook.DTO
{
    public class GradebookDto
    {
        public int Id { get; set; }
        public GroupDto Group { get; set; }
        public SubjectDto Subject { get; set; }
        public IEnumerable<GradeDto> Grades { get; set; }
        public IEnumerable<GradeDto> FinalGrades { get; set; }
    }
}