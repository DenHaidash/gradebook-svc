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
    public class SemesterSubjectsRepository : BaseRepository<SemesterSubject>, ISemesterSubjectsRepository
    {
        public SemesterSubjectsRepository(GradebookContext context) : base(context)
        {
        }

        public override async Task<SemesterSubject> GetByIdAsync(int id)
        {
            return await Set
                .Include(s => s.Subject)
                .Include(s => s.AssestmentType)
                .FirstOrDefaultAsync(s => s.SemesterRefId == id)
                .ConfigureAwait(false);
        }

        public override async Task<IEnumerable<SemesterSubject>> GetAllAsync()
        {
            return await Set
                .Include(s => s.Subject)
                .Include(s => s.AssestmentType)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public override async Task<IEnumerable<SemesterSubject>> GetAllAsync(Expression<Func<SemesterSubject, bool>> predicate)
        {
            return await Set
                .Include(s => s.Subject)
                .Include(s => s.AssestmentType)
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}