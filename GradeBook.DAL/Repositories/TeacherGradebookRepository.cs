using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public sealed class TeacherGradebookRepository : BaseRepository<GradebookTeacher>, ITeacherGradebookRepository
    {
        public TeacherGradebookRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<GradebookTeacher> WithIncludes(DbSet<GradebookTeacher> dbSet)
        {
            return dbSet
                .Include(s => s.Gradebook.Semester.Group)
                .Include(s => s.Gradebook.Subject);
        }

        protected override int GetKeyValue(GradebookTeacher entity)
        {
            return entity.TeacherRefId;
        }

        public async Task<IEnumerable<SemesterSubject>> GetTeacherSemesterGroups(int teacherId, int year, int semester)
        {
            return await Set
                .Where(s => s.TeacherRefId == teacherId 
                            && s.Gradebook.Semester.StartsAt.Year == (semester == 2 ? year + 1 : year)
                            && s.Gradebook.Semester.SemesterNumber == semester)
                .Select(s => s.Gradebook.SemesterSubject)
                .Include(s => s.Semester.Group.Speciality)
                .Include(s => s.Subject)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}