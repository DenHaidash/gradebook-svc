using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class CurriculumRepository : BaseRepository<Curriculum>, ICurriculumRepository
    {
        protected CurriculumRepository(GradebookContext context) : base(context)
        {
        }
    }
}