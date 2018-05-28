using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GradeBook.Models.Abstractions;

namespace GradeBook.Models
{
    [Table("Subjects")]
    public class Subject : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(30)]
        public string Name { get; set; }
        
        public virtual IEnumerable<SemesterSubject> Semesters { get; set; }
        public virtual IEnumerable<Gradebook> Gradebooks { get; set; }

    }
}