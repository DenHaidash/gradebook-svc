using System.Linq;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public class GroupsRepository : BaseRepository<Group>, IGroupsRepository
    {
        protected GroupsRepository(GradebookContext context) : base(context)
        {
        }
        
        public override async Task<Group> GetByIdAsync(int id)
        {
            return await _context.Set<Group>()
                .Include(g => g.Specialty)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
    }
}