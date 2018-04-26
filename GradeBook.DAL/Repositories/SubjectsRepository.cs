using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class SubjectsRepository : BaseRepository<Subject>, ISubjectsRepository
    {
        protected SubjectsRepository(GradebookContext context) : base(context)
        {
        }
    }
}