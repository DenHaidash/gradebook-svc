using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.UoW.Base
{
    public class UnitOfWork<TRepository> : IUnitOfWork<TRepository>
    {
        public UnitOfWork(TRepository repository, GradebookContext context)
        {
            DbContext = context;
            Repository = repository;
        }
 
        public TRepository Repository { get; private set; }
        protected GradebookContext DbContext { get; private set; }
        protected IUnitOfWorkTransaction Transaction { get; set; }
 
        public async Task<IUnitOfWorkTransaction> InTransactionAsync(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            return Transaction = new UnitOfWorkTransaction(await DbContext.Database.BeginTransactionAsync(level));
        }

        public async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }
 
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Transaction?.Dispose();
                Transaction = null;
                DbContext?.Dispose();
                DbContext = null;
            }
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}