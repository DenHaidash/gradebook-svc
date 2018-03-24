using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    public class CurriculumSubject
    {
        public int CurriculumRefId { get; set; }
        [ForeignKey("CurriculumRefId")]
        public Curriculum Curriculum { get; set; }
        public int SubjectRefId { get; set; }
        [ForeignKey("SubjectRefId")]        
        public Subject Subject { get; set; }
    }
}