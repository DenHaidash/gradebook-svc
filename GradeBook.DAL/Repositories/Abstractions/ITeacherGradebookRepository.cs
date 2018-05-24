using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories.Abstractions
{
    public interface ITeacherGradebookRepository : IRepository<GradebookTeacher>
    {
        Task<IEnumerable<SemesterSubject>> GetTeacherSemesterGroups(int teacherId, int year, int semester);
    }
}