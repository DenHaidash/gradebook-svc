using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Abstactions;

namespace GradeBook.Services
{
    public class StatisticService : IStatisticService
    {
        public Task<GradesStatsDto> GetStudentGradesStatsAsync(int studentId)
        {
            throw new System.NotImplementedException();
        }

        public Task<GradesStatsDto> GetGroupGradesStatsAsync(int groupId)
        {
            throw new System.NotImplementedException();
        }

        public Task<GradesStatsDto> GetGroupGradesStatsAsync(int groupId, int subjectId)
        {
            throw new System.NotImplementedException();
        }
    }
}