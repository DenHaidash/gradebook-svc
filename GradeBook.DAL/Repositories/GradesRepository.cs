using System.Linq;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public class GradesRepository : BaseRepository<Grade>, IGradesRepository
    {
        public GradesRepository(GradebookContext context) : base(context)
        {
        }

        public async Task<int> GetStudentSubjectCurrentGradeTotalAsync(int studentId, int subjectId)
        {
            return await Set
                .Where(s => s.StudentRefId == studentId
                            && s.Gradebook.SubjectRefId == subjectId)
                .SumAsync(s => s.Value);
        }

        protected override int GetKeyValue(Grade entity)
        {
            return entity.Id;
        }
    }
}