using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class GradesRepository : BaseRepository<Grade>, IGradesRepository
    {
        public GradesRepository(GradebookContext context) : base(context)
        {
        }
    }
}