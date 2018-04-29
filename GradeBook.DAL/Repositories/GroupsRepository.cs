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
    public class GroupsRepository : BaseRepository<Group>, IGroupsRepository
    {
        public GroupsRepository(GradebookContext context) : base(context)
        {
        }
        
        public override async Task<Group> GetByIdAsync(int id)
        {
            return await Set
                .Include(g => g.Specialty)
                .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted)
                .ConfigureAwait(false);
        }

        public override async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await Set
                .Include(g => g.Specialty)
                .Where(g => !g.IsDeleted)
                .ToListAsync()
                .ConfigureAwait(false);
        }
        
        public override async Task<IEnumerable<Group>> GetAllAsync(Expression<Func<Group, bool>> predicate)
        {
            return await Set
                .Include(g => g.Specialty)
                .Where(g => !g.IsDeleted)
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}