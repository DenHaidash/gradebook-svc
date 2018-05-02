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

        public override async Task<IEnumerable<GradebookTeacher>> GetAllAsync()
        {
            return await Set.Include(s => s.Gradebook.Semester).ToListAsync().ConfigureAwait(false);
        }

        public override async Task<IEnumerable<GradebookTeacher>> GetAllAsync(Expression<Func<GradebookTeacher, bool>> predicate)
        {
            return await Set
                .Include(s => s.Gradebook.Semester.Group)
                .Include(s => s.Gradebook.Subject)
                .Where(predicate)
                .ToListAsync()
                .ConfigureAwait(false);
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