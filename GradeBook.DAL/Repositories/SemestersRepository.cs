using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public sealed class SemestersRepository : BaseRepository<Semester>, ISemestersRepository
    {
        public SemestersRepository(GradebookContext context) : base(context)
        {
        }

        protected override int GetKeyValue(Semester entity)
        {
            return entity.Id;
        }
    }
}