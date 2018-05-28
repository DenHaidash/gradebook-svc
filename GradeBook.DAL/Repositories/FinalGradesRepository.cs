using System.Linq;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public sealed class FinalGradesRepository : BaseRepository<FinalGrade>, IFinalGradesRepository
    {
        public FinalGradesRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<FinalGrade> WithIncludes(DbSet<FinalGrade> dbSet)
        {
            return dbSet.Include(s => s.Teacher.Account)
                .Include(s => s.Gradebook.Semester)
                .Include(s => s.Gradebook.Subject)
                .Include(s => s.Gradebook.SemesterSubject.AssestmentType);
        }
    }
}