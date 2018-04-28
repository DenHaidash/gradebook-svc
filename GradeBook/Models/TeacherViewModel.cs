using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GradeBook.DTO;

namespace GradeBook.Models
{
    public class TeacherViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string Email { get; set; }
        public IEnumerable<SubjectDto> Specializations { get; set; } = Enumerable.Empty<SubjectDto>();
    }
}