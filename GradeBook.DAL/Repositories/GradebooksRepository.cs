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
    public class GradebooksRepository : BaseRepository<Gradebook>, IGradebooksRepository
    {
        public GradebooksRepository(GradebookContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Gradebook>> GetAllAsync(Expression<Func<Gradebook, bool>> predicate)
        {
            return await Set.Include(s => s.Semester).Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        public override async Task<IEnumerable<Gradebook>> GetAllAsync()
        {
            return await Set.Include(s => s.Semester).ToListAsync().ConfigureAwait(false);
        }

        public override async Task<Gradebook> GetByIdAsync(int id)
        {
            return await Set.Include(s => s.Semester).FirstOrDefaultAsync(s => s.Id == id).ConfigureAwait(false);
        }
    }
}