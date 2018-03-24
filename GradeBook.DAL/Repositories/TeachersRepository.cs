using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class TeachersRepository : BaseRepository<Teacher>, ITeachersRepository
    {
        protected TeachersRepository(GradebookContext context) : base(context)
        {
        }
    }
}