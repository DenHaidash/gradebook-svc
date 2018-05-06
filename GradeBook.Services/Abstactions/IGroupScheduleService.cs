using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IGroupScheduleService
    {
        Task<IEnumerable<SemesterSubjectDto>> GetGroupSemesterSubjects(int groupId, int yearNumber, int semesterNumber);
        Task<IEnumerable<SemesterSubjectDto>> GetStudentGroupCurrentSemesterSubjects(int studentId);
        Task<bool> HasGroupSubjectInScheduleAsync(int groupId, int subjectId);
        Task AddSubjectToSemester(SemesterSubjectDto semesterSubject, int groupId, int yearNumber, int semesterNumber);
        Task DeleteSubjectFromSemester(int subjectId, int groupId, int yearNumber, int semesterNumber);
    }
}