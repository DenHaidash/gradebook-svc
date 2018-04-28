using System.Collections.Generic;
using System.Linq;

namespace GradeBook.DTO
{
    public class TeacherDto : AccountDto
    {        
        public IEnumerable<SubjectDto> Specializations { get; set; } = Enumerable.Empty<SubjectDto>();
    }
}