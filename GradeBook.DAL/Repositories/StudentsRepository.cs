using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public class StudentsRepository : BaseRepository<Student>, IStudentsRepository
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

        public override async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await WithIncludes(Set)
                .Where(s => !s.IsDeleted)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public override async Task<IEnumerable<Student>> GetAllAsync(Expression<Func<Student, bool>> predicate)
        {
            return await WithIncludes(Set)
                .Where(s => !s.IsDeleted)
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        protected override int GetKeyValue(Student entity)
        {
            return entity.Id;
        }

        public override async Task<Student> GetByIdAsync(int id)
        {
            return await WithIncludes(Set)
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted)
                .ConfigureAwait(false);
        }
    }
}