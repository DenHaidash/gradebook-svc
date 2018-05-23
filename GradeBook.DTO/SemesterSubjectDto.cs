using System.Collections.Generic;

namespace GradeBook.DTO
{
    public class SemesterSubjectDto
    {     
        public SubjectDto Subject { get; set; }
        public AssessmentTypeDto AssessmentType { get; set; }
        public IEnumerable<TeacherDto> Teachers { get; set; }
    }
}