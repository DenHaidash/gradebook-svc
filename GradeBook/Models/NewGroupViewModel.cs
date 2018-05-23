using System;
using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class NewGroupViewModel : GroupViewModel
    {
        [DataType(DataType.Date)]
        public DateTime EducationStartedAt { get; set; }
    }
}