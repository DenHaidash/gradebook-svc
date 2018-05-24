using System.Collections.Generic;

namespace GradeBook.DTO
{
    public class GroupSubjectsDto
    {
        public GroupDto Group { get; set; }
        public IEnumerable<SubjectDto> Subjects { get; set; }
    }
}