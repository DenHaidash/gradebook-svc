using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class GradebooksRepository : BaseRepository<Gradebook>, IGradebooksRepository
    {
        public GradebooksRepository(GradebookContext context) : base(context)
        {
        }
    }
}