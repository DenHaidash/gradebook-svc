using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    public class SemesterSubject
    {
        public int SemesterRefId { get; set; }
        [ForeignKey("SemesterRefId")]
        public Semester Semester { get; set; }
        public int SubjectRefId { get; set; }
        [ForeignKey("SubjectRefId")]        
        public Subject Subject { get; set; }
        public int AssestemtTypeRefId { get; set; }
        [ForeignKey("AssestemtTypeRefId")]   
        public AssestmentType AssestmentType { get; set; }
    }
}