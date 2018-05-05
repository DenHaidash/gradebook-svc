using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    [Table("SemestersSubjects")]
    public class SemesterSubject
    {
        public int SemesterRefId { get; set; }
        
        [ForeignKey("SemesterRefId")]
        public Semester Semester { get; set; }
        
        public int SubjectRefId { get; set; }
        
        [ForeignKey("SubjectRefId")]        
        public Subject Subject { get; set; }
        
        public int AssestmentTypeRefId { get; set; }
        
        [ForeignKey("AssestmentTypeRefId")]   
        public AssestmentType AssestmentType { get; set; }
    }
}