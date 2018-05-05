using System.Linq;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace GradeBook.DAL.Repositories
{
    public class FinalGradesRepository : BaseRepository<FinalGrade>, IFinalGradesRepository
    {
        public FinalGradesRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<FinalGrade> WithIncludes(DbSet<FinalGrade> dbSet)
        {
            return dbSet.Include(s => s.Teacher);
        }

        protected override int GetKeyValue(FinalGrade entity)
        {
            return entity.Id;
        }
    }
}