using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    [Table("SemestersSubjects")]
    public class SemesterSubject
    {
        public int SemesterRefId { get; set; }
        
        [ForeignKey("SemesterRefId")]
        public virtual Semester Semester { get; set; }
        
        public int SubjectRefId { get; set; }
        
        [ForeignKey("SubjectRefId")]        
        public virtual Subject Subject { get; set; }
        
        public int AssestmentTypeRefId { get; set; }
        
        [ForeignKey("AssestmentTypeRefId")]   
        public virtual AssestmentType AssestmentType { get; set; }
        
        public virtual IEnumerable<Gradebook> Gradebooks { get; set; }
    }
}