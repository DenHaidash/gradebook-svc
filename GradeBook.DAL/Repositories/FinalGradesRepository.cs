using System.Linq;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public class FinalGradesRepository : BaseRepository<FinalGrade>, IFinalGradesRepository
    {
        public FinalGradesRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<FinalGrade> WithIncludes(DbSet<FinalGrade> dbSet)
        {
            return dbSet.Include(s => s.Teacher.Account)
                .Include(s => s.Gradebook.SemesterSubject.AssestmentType);
        }

        protected override int GetKeyValue(FinalGrade entity)
        {
            return entity.Id;
        }
    }
}