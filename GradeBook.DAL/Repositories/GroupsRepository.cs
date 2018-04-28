using System.Linq;
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
            return await Context.Groups
                .Include(g => g.Specialty)
                .FirstOrDefaultAsync(g => g.Id == id)
                .ConfigureAwait(false);
        }
    }
}