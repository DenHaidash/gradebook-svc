﻿using System;

namespace GradeBook.DTO
{
    public class SemesterDto
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public int CourseNumber { get; set; }
        public int SemesterNumber { get; set; }
        public DateTime StartsAt { get; set; }
        public DateTime EndsAt { get; set; }
    }
}