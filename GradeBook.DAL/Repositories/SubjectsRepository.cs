using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class SubjectsRepository : BaseRepository<Subject>, ISubjectsRepository
    {
        public SubjectsRepository(GradebookContext context) : base(context)
        {
        }

        protected override int GetKeyValue(Subject entity)
        {
            return entity.Id;
        }
    }
}