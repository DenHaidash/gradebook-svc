using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public sealed class SpecialitiesRepository : BaseRepository<Specialty>, ISpecialitiesRepository
    {
        public SpecialitiesRepository(GradebookContext context) : base(context)
        {
        }

        protected override int GetKeyValue(Specialty entity)
        {
            return entity.Id;
        }
    }
}