using System.Linq;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public class SemestersRepository : BaseRepository<Semester>, ISemestersRepository
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