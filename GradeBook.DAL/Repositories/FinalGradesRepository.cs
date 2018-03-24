using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class FinalGradesRepository : BaseRepository<FinalGrade>, IFinalGradesRepository
    {
        protected FinalGradesRepository(GradebookContext context) : base(context)
        {
        }
    }
}