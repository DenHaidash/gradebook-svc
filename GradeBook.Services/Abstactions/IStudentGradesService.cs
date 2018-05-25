using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IStudentGradesService
    {
        Task<IEnumerable<FinalGradeDto>> GetStudentFinalGradesAsync(int studentId);
        Task<FinalGradeDto> GetStudentSubjectFinalGradeAsync(int studentId, int subjectId);
        Task<StudentSubjectGradesDto> GetStudentSubjectCurrentGradesAsync(int studentId, int subjectId);
        
        Task<GradeDto> GetGradeAsync(int gradeId);
        Task<FinalGradeDto> GetFinalGradeAsync(int finalGradeId);

        Task<GradeDto> AddStudentCourseGradeAsync(GradeDto grade, int studentId, int teacherId, int subjectId);
        Task RemoveStudentCourseGradeAsync(int gradeId, int teacherId);
        Task<FinalGradeDto> ConfirmStudentCourseFinalGradeAsync(int studentId, int teacherId, int subjectId);
        Task<int> GetStudentCourseCurrentGradeTotalAsync(int studentId, int subjectId);
        
    }
}