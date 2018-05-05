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
    public class GradebooksRepository : BaseRepository<Gradebook>, IGradebooksRepository
    {
        public GradebooksRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<Gradebook> WithIncludes(DbSet<Gradebook> dbSet)
        {
            return dbSet
                .Include(s => s.Semester)
                .Include(s => s.GradebookTeachers)
                .ThenInclude(s => s.Teacher);
        }

        protected override int GetKeyValue(Gradebook entity)
        {
            return entity.Id;
        }
    }
}