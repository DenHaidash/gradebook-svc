using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class SpecialitiesRepository : BaseRepository<Specialty>, ISpecialitiesRepository
    {
        public SpecialitiesRepository(GradebookContext context) : base(context)
        {
        }
    }
}