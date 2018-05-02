using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IGroupSemestersService
    {
        Task<IEnumerable<SemesterDto>> GetGroupSemestersAsync(int groupId);
        Task<SemesterDto> GetGroupSemesterAsync(int groupId, int year, int semester);
        Task<int> CreateGroupSemesterAsync(SemesterDto semester);
        Task CreateGroupSemestersAsync(IEnumerable<SemesterDto> semester);
        Task DeleteGroupSemester(int semesterId);
    }
}