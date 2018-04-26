namespace GradeBook.DTO
{
    public class StudentDto : AccountDto
    {
        public virtual GroupDto Group { get; set; }
    }
}