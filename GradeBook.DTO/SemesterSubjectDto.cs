using System.Collections.Generic;

namespace GradeBook.DTO
{
    public class SemesterSubjectDto
    {     
        public SubjectDto Subject { get; set; }
        public AssestmentTypeDto AssestmentType { get; set; }
        public IEnumerable<TeacherDto> Teachers { get; set; }
    }
}