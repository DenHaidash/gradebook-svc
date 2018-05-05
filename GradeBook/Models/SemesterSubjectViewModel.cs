using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class SemesterSubjectViewModel
    {
        [Range(1, int.MaxValue)]
        public int SubjectId { get; set; }
        
        [Range(1, int.MaxValue)]
        public int AssestmentTypeId { get; set; }
    }
}