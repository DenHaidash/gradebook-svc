using System.Threading.Tasks;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories.Abstractions
{
    public interface IGradesRepository : IRepository<Grade>
    {
        Task<int> GetStudentSubjectCurrentGradeTotalAsync(int studentId, int subjectId);
    }
}