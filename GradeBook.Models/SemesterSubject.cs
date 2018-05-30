using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GradeBook.Models.Abstractions;

namespace GradeBook.Models
{
    [Table("SemestersSubjects")]
    public class SemesterSubject : IEntity
    {
        public int Id { get; set; }
        
        public int SemesterRefId { get; set; }
        
        [ForeignKey("SemesterRefId")]
        public virtual Semester Semester { get; set; }
        
        public int SubjectRefId { get; set; }
        
        [ForeignKey("SubjectRefId")]        
        public virtual Subject Subject { get; set; }
        
        public int AssestmentTypeRefId { get; set; }
        
        [ForeignKey("AssestmentTypeRefId")]   
        public virtual AssestmentType AssestmentType { get; set; }
        
        public virtual Gradebook Gradebook { get; set; }        
    }
}