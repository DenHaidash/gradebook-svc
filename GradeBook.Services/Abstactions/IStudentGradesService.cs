using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IStudentGradesService
    {
        Task<IEnumerable<GradeDto>> GetStudentFinalGradesAsync(int studentId);
        Task<GradeDto> GetStudentSubjectFinalGradeAsync(int studentId, int subjectId);
        Task<StudentSubjectGradesDto> GetStudentSubjectCurrentGradesAsync(int studentId, int subjectId);

        Task AddStudentCourseGradeAsync(GradeDto grade, int studentId, int teacherId, int subjectId);
        Task RemoveStudentCourseGradeAsync(int gradeId, int teacherId);
        Task ConfirmStudentCourseFinalGradeAsync(int studentId, int teacherId, int subjectId);
        Task<int> GetStudentCourseCurrentGradeTotalAsync(int studentId, int subjectId);
        
    }
}