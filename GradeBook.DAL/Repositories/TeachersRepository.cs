using System.Linq;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public sealed class TeachersRepository : BaseRepository<Teacher>, ITeachersRepository
    {
        public TeachersRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<Teacher> WithIncludes(DbSet<Teacher> dbSet)
        {
            return dbSet.Include(t => t.Account);
        }
    }
}