using System.Linq;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public sealed class GroupsRepository : BaseRepository<Group>, IGroupsRepository
    {
        public GroupsRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<Group> WithIncludes(DbSet<Group> dbSet)
        {
            return dbSet.Include(g => g.Speciality);
        }
    }
}