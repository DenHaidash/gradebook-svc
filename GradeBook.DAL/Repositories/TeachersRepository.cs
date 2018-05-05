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
    public class TeachersRepository : BaseRepository<Teacher>, ITeachersRepository
    {
        public TeachersRepository(GradebookContext context) : base(context)
        {
        }

        protected override IQueryable<Teacher> WithIncludes(DbSet<Teacher> dbSet)
        {
            return dbSet
                .Include(t => t.Account);
        }

        protected override int GetKeyValue(Teacher entity)
        {
            return entity.Id;
        }
    }
}