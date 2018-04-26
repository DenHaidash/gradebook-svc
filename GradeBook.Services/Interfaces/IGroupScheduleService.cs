using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Interfaces
{
    public interface IGroupScheduleService
    {
        Task<IEnumerable<SemesterDto>> GetGroupSemestersAsync(int groupId);
        Task<int> CreateGroupSemesterAsync(SemesterDto semester);
        Task DeleteGroupSemester(int semesterId);

        Task<IEnumerable<SubjectDto>> GetGroupSemestedSubjects(int groupId, int yearNumber, int semesterNumber);
        Task AddSubjectToSemester(int subjectId, int groupId, int yearNumber, int semesterNumber);
        Task DeleteSubjectFromSemester(int subjectId, int groupId, int yearNumber, int semesterNumber);
    }
}