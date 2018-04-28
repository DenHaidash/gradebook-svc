using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories
{
    public class StudentsRepository : BaseRepository<Student>, IStudentsRepository
    {
        public StudentsRepository(GradebookContext context) : base(context)
        {
        }
    }
}