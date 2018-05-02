using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IGroupScheduleService
    {
        Task<IEnumerable<SemesterSubjectDto>> GetGroupSemestedSubjects(int groupId, int yearNumber, int semesterNumber);
        Task AddSubjectToSemester(SemesterSubjectDto semesterSubject, int groupId, int yearNumber, int semesterNumber);
        Task DeleteSubjectFromSemester(int subjectId, int groupId, int yearNumber, int semesterNumber);
    }
}