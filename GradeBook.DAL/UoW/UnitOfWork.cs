using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.UoW
{
    public class UnitOfWork<TRepository> : IUnitOfWork<TRepository>
    {
        public UnitOfWork(TRepository repository, GradebookContext context)
        {
            DbContext = context;
            Repository = repository;
            
            Transactions= new List<IUnitOfWorkTransaction>();
        }
 
        public TRepository Repository { get; private set; }
        protected GradebookContext DbContext { get; private set; }
        protected List<IUnitOfWorkTransaction> Transactions { get; private set; }
 
        public async Task<IUnitOfWorkTransaction> BeginTransactionAsync(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            var transaction = new UnitOfWorkTransaction(await DbContext.Database.BeginTransactionAsync(level));
            
            Transactions.Add(transaction);
            
            return transaction;
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }
 
        public virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            
            Transactions.ForEach(t => t.Dispose());
            Transactions.RemoveAll(t => true);
                
            DbContext?.Dispose();
            DbContext = null;
        }
 
        public void Dispose()
        {
            Dispose(true);
            
            GC.SuppressFinalize(this);
        }
    }
}