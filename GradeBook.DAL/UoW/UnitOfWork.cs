using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using GradeBook.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

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
            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch(DbUpdateException ex) when(ex.InnerException is PostgresException 
                                             && ex.InnerException.Message.Contains("23505"))
            {
                throw new ResourceDatabaseOperationException("Duplicated record", ex);
            }
            catch(DbUpdateException ex) when(ex.InnerException is PostgresException 
                                             && ex.InnerException.Message.Contains("23503"))
            {
                throw new ResourceDatabaseOperationException("Foreign key violation", ex);
            }
            catch (Exception ex)
            {
                throw new ResourceDatabaseOperationException("Database operation error", ex);
            }
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