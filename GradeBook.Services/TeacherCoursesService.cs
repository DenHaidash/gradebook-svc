using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Interfaces;

namespace GradeBook.Services
{
    public class TeacherCoursesService : ITeacherCoursesService
    {
        public Task<IEnumerable<GroupDto>> GetTeacherSemesterGroupsAsync(int teacherId, int semesterId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SubjectDto>> GetTeacherSemesterGroupCoursesAsync(int teacherId, int semesterId, int groupId)
        {
            throw new System.NotImplementedException();
        }

        public Task AssignTeacherToCourseAsync(int teacherId, int semesterId, int groupId, int subjectId)
        {
            throw new System.NotImplementedException();
        }

        public Task UnassignTeacherFromCourseAsync(int teacherId, int semesterId, int groupId, int subjectId)
        {
            throw new System.NotImplementedException();
        }
    }
}