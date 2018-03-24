using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class GroupsRepository : BaseRepository<Group>, IGroupsRepository
    {
        protected GroupsRepository(GradebookContext context) : base(context)
        {
        }
    }
}