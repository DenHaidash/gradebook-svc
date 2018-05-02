using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class FinalGradesRepository : BaseRepository<FinalGrade>, IFinalGradesRepository
    {
        public FinalGradesRepository(GradebookContext context) : base(context)
        {
        }
    }
}