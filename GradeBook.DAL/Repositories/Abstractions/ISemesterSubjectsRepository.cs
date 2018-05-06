using System.Threading.Tasks;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories.Abstractions
{
    public interface ISemesterSubjectsRepository : IRepository<SemesterSubject>
    {
        Task<bool> HasGroupSubjectInScheduleAsync(int groupId, int subjectId);
    }
}