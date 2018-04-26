using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Interfaces;

namespace GradeBook.Services
{
    public class GroupScheduleService : IGroupScheduleService
    {
        public GroupScheduleService()
        {
            
        }
        
        public Task<IEnumerable<SemesterDto>> GetGroupSemestersAsync(int groupId)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> CreateGroupSemesterAsync(SemesterDto semester)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteGroupSemester(int semesterId)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SubjectDto>> GetGroupSemestedSubjects(int groupId, int yearNumber, int semesterNumber)
        {
            throw new System.NotImplementedException();
        }

        public Task AddSubjectToSemester(int subjectId, int groupId, int yearNumber, int semesterNumber)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteSubjectFromSemester(int subjectId, int groupId, int yearNumber, int semesterNumber)
        {
            throw new System.NotImplementedException();
        }
    }
}