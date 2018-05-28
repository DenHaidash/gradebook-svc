using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GradeBook.Models.Abstractions;

namespace GradeBook.Models
{
    [Table("Groups")]
    public class Group : IEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required, MaxLength(10)]
        public string Code { get; set; }
        
        public int SpecialityRefId { get; set; }
        
        [ForeignKey("SpecialityRefId")]
        public virtual Specialty Speciality { get; set; }
        
        public virtual IEnumerable<Student> Students { get; set; }
        
        public virtual IEnumerable<Semester> Semesters { get; set; }
    }
}