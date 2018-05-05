using System.ComponentModel.DataAnnotations;

namespace GradeBook.Models
{
    public class NewStudentViewModel : NewAccountViewModel
    {
        [Range(1, int.MaxValue)]
        public int GroupId { get; set; }
    }
}