using System;
using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class GradeViewModel
    {
        [Range(-100, 100)]
        public int Value { get; set; }
        
        [Required, StringLength(50)]
        public string Description { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }
    }
}