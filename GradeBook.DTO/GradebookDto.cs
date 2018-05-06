using System.Collections.Generic;

namespace GradeBook.DTO
{
    public class GradebookDto
    {
        public int Id { get; set; }
        public int SemesterId { get; set; }
        public int SubjectId { get; set; }
        public IEnumerable<TeacherDto> Teachers { get; set; }
        public AssestmentTypeDto AssestmentType { get; set; }
    }
}