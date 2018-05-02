using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Abstactions;

namespace GradeBook.Services
{
    public class StudentGradesService : IStudentGradesService
    {
        public Task<IEnumerable<GradeDto>> GetStudentFinalGradesAsync(int studentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<GradeDto> GetStudentSubjectFinalGradeAsync(int studentId, int subjectId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<GradeDto>> GetStudentSubjectCurrentGradesAsync(int studentId, int subjectId)
        {
            throw new System.NotImplementedException();
        }

        public Task AddStudentCourseGradeAsync(GradeDto grade, int studentId)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveStudentCourseGradeAsync(int gradeId, int studentId)
        {
            throw new System.NotImplementedException();
        }

        public Task ConfirmStudentCourseFinalGradeAsync(int studentId, int courseId)
        {
            throw new System.NotImplementedException();
        }
    }
}