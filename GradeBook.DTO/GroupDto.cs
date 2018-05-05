using System;

namespace GradeBook.DTO
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public SpecialtyDto Specialty { get; set; }
    }
}