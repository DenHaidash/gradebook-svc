using System;
using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class GroupViewModel
    {
        [Required, StringLength(10)]
        public string Code { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime EducationStartedAt { get; set; }
        
        [Range(1, int.MaxValue)]
        public int SpecialtyId { get; set; }
    }
}