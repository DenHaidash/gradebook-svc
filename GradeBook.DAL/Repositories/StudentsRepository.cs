using System.Linq;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public sealed class StudentsRepository : BaseRepository<Student>, IStudentsRepository
    {
        public StudentsRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<Student> WithIncludes(DbSet<Student> dbSet)
        {
            return dbSet
                .Include(s => s.Account)
                .Include(s => s.Group.Specialty);
        }

        protected override int GetKeyValue(Student entity)
        {
            return entity.Id;
        }
    }
}