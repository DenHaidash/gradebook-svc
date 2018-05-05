using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public class TeacherGradebookRepository : BaseRepository<GradebookTeacher>, ITeacherGradebookRepository
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

        public async Task<IEnumerable<Group>> GetTeacherSemesterGroups(int teacherId, int year, int semester)
        {
            return await Set
                .Where(s => s.TeacherRefId == teacherId 
                            && s.Gradebook.Semester.StartsAt.Year == year
                            && s.Gradebook.Semester.SemesterNumber == semester)
                .Select(s => s.Gradebook.Semester.Group)
                .Include(s => s.Specialty)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}