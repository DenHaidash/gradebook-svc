using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GradeBook.Models.Abstractions;

namespace GradeBook.Models
{
    [Table("Semesters")]
    public class Semester : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        public int GroupRefId { get; set; }
        
        [ForeignKey("GroupRefId")]
        public virtual Group Group { get; set; }
        
        public int CourseNumber { get; set; }
        
        public int SemesterNumber { get; set; }
        
        public DateTime StartsAt { get; set; }
        
        public DateTime EndsAt { get; set; }
        
        public virtual IEnumerable<SemesterSubject> SemesterSubjects { get; set; }
        public virtual IEnumerable<Gradebook> Gradebooks { get; set; }
    }
}