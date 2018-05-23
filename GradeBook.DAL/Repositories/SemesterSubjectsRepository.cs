using System.Linq;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public sealed class SemesterSubjectsRepository : BaseRepository<SemesterSubject>, ISemesterSubjectsRepository
    {
        public SemesterSubjectsRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<SemesterSubject> WithIncludes(DbSet<SemesterSubject> dbSet)
        {
            return dbSet
                .Include(s => s.Gradebooks)
                .ThenInclude(g => g.GradebookTeachers)
                .ThenInclude(t => t.Teacher.Account)
                .Include(s => s.Subject)
                .Include(s => s.AssestmentType);
        }

        protected override int GetKeyValue(SemesterSubject entity)
        {
            return entity.SemesterRefId;
        }

        public async Task<bool> HasGroupSubjectInScheduleAsync(int groupId, int subjectId)
        {
            return await Set.AnyAsync(s => s.SubjectRefId == subjectId && s.Semester.GroupRefId == groupId)
                .ConfigureAwait(false);
        }
    }
}