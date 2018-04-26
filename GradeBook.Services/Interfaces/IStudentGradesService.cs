﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Interfaces
{
    public interface IStudentGradesService
    {
        Task<IEnumerable<GradeDto>> GetStudentFinalGradesAsync(int studentId);
        Task<GradeDto> GetStudentSubjectFinalGradeAsync(int studentId, int subjectId);
        Task<IEnumerable<GradeDto>> GetStudentSubjectCurrentGradesAsync(int studentId, int subjectId);

        Task AddStudentCourseGradeAsync(GradeDto grade, int studentId);
        Task RemoveStudentCourseGradeAsync(int gradeId, int studentId);
        Task ConfirmStudentCourseFinalGradeAsync(int studentId, int courseId);
        
    }
}