using System;
using System.Collections.Generic;
using System.Linq;
using GradeBook.DTO;

namespace GradeBook.Services.Helpers
{
    public static class SemestersHelper
    {
        public static IEnumerable<SemesterDto> GenerateSemesters(int fromYear, int numberOfCourses = 6)
        {   
            return Enumerable.Range(fromYear, numberOfCourses).Select((year, index) => new []
            {
                new SemesterDto
                {
                    StartsAt = new DateTime(year, 9, 1),
                    EndsAt = new DateTime(year + 1, 1, 31),
                    SemesterNumber = 1,
                    CourseNumber = index + 1
                },
                new SemesterDto
                {
                    StartsAt = new DateTime(year + 1, 2, 1),
                    EndsAt = new DateTime(year + 1, 6, 30),
                    SemesterNumber = 2,
                    CourseNumber = index + 1
                }
            }).SelectMany(s => s);
        }

        public static (int year, int semester) IdentifySemester(DateTime date)
        {
            var semester = date.Month > 1 && date.Month < 9 ? 2 : 1;

            return (year: date.Year, semester);
        }
    }
}