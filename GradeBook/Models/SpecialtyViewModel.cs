﻿using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class SpecialtyViewModel
    {
        [Required, StringLength(20)]
        public string Code { get; set; }
        
        [Required, StringLength(50)]
        public string Name { get; set; }
    }
}