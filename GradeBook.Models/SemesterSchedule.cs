using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GradeBook.Models
{
    public class SemesterSchedule
    {
        [Key]
        public int Id { get; set; }
        public int GroupRefId { get; set; }
        [ForeignKey("GroupRefId")]
        public virtual Group Group { get; set; }
        public int SemesteRefId { get; set; }
        [ForeignKey("SemesteRefId")]
        public virtual Semester Semester { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
        public virtual IEnumerable<SemesterSchuduleSubject> SemesterSubjects { get; set; }
    }
}