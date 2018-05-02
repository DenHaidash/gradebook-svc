using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface ITeacherCoursesService
    {
        Task<IEnumerable<GroupDto>> GetTeacherSemesterGroupsAsync(int teacherId, int year, int semester);
        Task<IEnumerable<SubjectDto>> GetTeacherSemesterGroupCoursesAsync(int teacherId, int year, int semester, int groupId);
        Task AssignTeacherToCourseAsync(int teacherId, int year, int semester, int groupId, int subjectId);
        Task UnassignTeacherFromCourseAsync(int teacherId, int year, int semester, int groupId, int subjectId);
    }
}