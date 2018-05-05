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
    public class GroupsRepository : BaseRepository<Group>, IGroupsRepository
    {
        public GroupsRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<Group> WithIncludes(DbSet<Group> dbSet)
        {
            return dbSet.Include(g => g.Specialty);
        }

        protected override int GetKeyValue(Group entity)
        {
            return entity.Id;
        }

        public override async Task<Group> GetByIdAsync(int id)
        {
            return await WithIncludes(Set)
                .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted)
                .ConfigureAwait(false);
        }

        public override async Task<IEnumerable<Group>> GetAllAsync()
        {
            return await WithIncludes(Set)
                .Where(g => !g.IsDeleted)
                .ToListAsync()
                .ConfigureAwait(false);
        }
        
        public override async Task<IEnumerable<Group>> GetAllAsync(Expression<Func<Group, bool>> predicate)
        {
            return await WithIncludes(Set)
                .Where(g => !g.IsDeleted)
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}