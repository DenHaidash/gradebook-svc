using System;
using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class TeacherSubjectAssignmentViewModel
    {
        [Range(1, int.MaxValue)]
        public int TeacherId { get; set; }
    }
}