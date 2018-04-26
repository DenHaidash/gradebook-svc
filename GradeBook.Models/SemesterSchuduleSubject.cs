using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    public class SemesterSchuduleSubject
    {
        public int SemesterScheduleRefId { get; set; }
        [ForeignKey("SemesterScheduleRefId")]
        public SemesterSchedule SemesterSchedule { get; set; }
        public int SubjectRefId { get; set; }
        [ForeignKey("SubjectRefId")]        
        public Subject Subject { get; set; }
        public int AssestemtTypeRefId { get; set; }
        [ForeignKey("AssestemtTypeRefId")]   
        public AssestmentType AssestmentType { get; set; }
    }
}