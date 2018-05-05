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
    public class SemestersRepository : BaseRepository<Semester>, ISemestersRepository
    {
        public SemestersRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<Semester> WithIncludes(DbSet<Semester> dbSet)
        {
            return dbSet.Include(s => s.Group.Specialty);
        }

        protected override int GetKeyValue(Semester entity)
        {
            return entity.Id;
        }
    }
}