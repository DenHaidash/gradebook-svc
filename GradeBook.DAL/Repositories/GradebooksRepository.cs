using System.Linq;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public sealed class GradebooksRepository : BaseRepository<Gradebook>, IGradebooksRepository
    {
        public GradebooksRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<Gradebook> WithIncludes(DbSet<Gradebook> dbSet)
        {
            return dbSet
                .Include(s => s.Semester)
                .Include(s => s.SemesterSubject.AssestmentType)
                .Include(s => s.GradebookTeachers)
                .ThenInclude(s => s.Teacher);
        }

        protected override int GetKeyValue(Gradebook entity)
        {
            return entity.Id;
        }
    }
}