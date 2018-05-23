using System;
using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class GroupViewModel
    {
        [Required, StringLength(10)]
        public string Code { get; set; }
        
        [Range(1, int.MaxValue)]
        public int SpecialityId { get; set; }
    }
}