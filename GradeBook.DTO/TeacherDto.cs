using System.Collections.Generic;

namespace GradeBook.DTO
{
    public class TeacherDto : AccountDto
    {
        public virtual IEnumerable<SubjectDto> Specializations { get; set; }
    }
}