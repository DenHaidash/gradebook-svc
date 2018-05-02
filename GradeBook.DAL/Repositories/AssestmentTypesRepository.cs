using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class AssestmentTypesRepository : BaseRepository<AssestmentType>, IAssestmentTypesRepository
    {
        public AssestmentTypesRepository(GradebookContext context) : base(context)
        {
        }
    }
}