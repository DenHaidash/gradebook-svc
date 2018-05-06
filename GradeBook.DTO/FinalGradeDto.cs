using System;

namespace GradeBook.DTO
{
    public class FinalGradeDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public TeacherDto Teacher { get; set; }
        public AssestmentTypeDto AssestmentType { get; set; }
    }
}