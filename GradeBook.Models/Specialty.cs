using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class Specialty
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual IEnumerable<Group> Groups { get; set; }
        public virtual IEnumerable<Curriculum> Curriculum { get; set; }
    }
}