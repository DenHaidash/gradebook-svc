using System.Collections.Generic;
using System.Linq;

namespace GradeBook.DTO
{
    public class StudentSubjectGradesDto
    {
        public AssestmentTypeDto AssestmentType { get; set; }
        public FinalGradeDto FinalGrade { get; set; }
        public IEnumerable<GradeDto> CurrentGrades { get; set; } = new List<GradeDto>();
        
        public int CurrentGradesTotal => CurrentGrades.Sum(s => s.Value);
    }
}