using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface ITeacherCoursesService
    {
        Task<IEnumerable<GroupSubjectsDto>> GetTeacherSemesterGroupsAsync(int teacherId, int year, int semester);
        Task<IEnumerable<GroupSubjectsDto>> GetTeacherCurrentSemesterGroupsAsync(int teacherId);
        Task<IEnumerable<SubjectDto>> GetTeacherSemesterGroupCoursesAsync(int teacherId, int year, int semester, int groupId);
        Task<IEnumerable<SubjectDto>> GetTeacherCurrentSemesterGroupCoursesAsync(int teacherId, int groupId);
        Task<IEnumerable<TeacherDto>> GetTeachersOfCourseAsync(int groupId, int subjectId);
        Task AssignTeacherToCourseAsync(int teacherId, int groupId, int subjectId);
        Task UnassignTeacherFromCourseAsync(int teacherId, int year, int semester, int groupId, int subjectId);
    }
}