using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public class SemestersRepository : BaseRepository<Semester>, ISemestersRepository
    {
        public SemestersRepository(GradebookContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Semester>> GetAllAsync()
        {
            return await Set.Include(s => s.Group.Specialty).ToListAsync().ConfigureAwait(false);
        }

        public override async Task<IEnumerable<Semester>> GetAllAsync(Expression<Func<Semester, bool>> predicate)
        {
            return await Set.Include(s => s.Group.Specialty)
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public override async Task<Semester> GetByIdAsync(int id)
        {
            return await Set.Include(s => s.Group.Specialty)
                .FirstOrDefaultAsync(s => s.Id == id)
                .ConfigureAwait(false);
        }
    }
}