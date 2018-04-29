using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly GradebookContext _context;

        protected BaseRepository(GradebookContext context)
        {
            _context = context;
        }

        protected DbSet<TEntity> Set => _context.Set<TEntity>();

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await Set.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Set.ToListAsync().ConfigureAwait(false);
        }
        
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Set.Where(predicate).ToListAsync().ConfigureAwait(false);
        }

        public virtual void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            Set.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            Set.Attach(entity);
            Set.Remove(entity);
        }
    }
}