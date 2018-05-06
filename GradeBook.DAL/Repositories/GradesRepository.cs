using System.Linq;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public sealed class GradesRepository : BaseRepository<Grade>, IGradesRepository
    {
        public GradesRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<Grade> WithIncludes(DbSet<Grade> dbSet)
        {
            return dbSet
                .Include(s => s.Gradebook)
                .Include(s => s.Teacher.Account);
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