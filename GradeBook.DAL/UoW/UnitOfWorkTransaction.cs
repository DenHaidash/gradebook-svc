using System;
using Microsoft.EntityFrameworkCore.Storage;

namespace GradeBook.DAL.UoW
{
    public class UnitOfWorkTransaction : IUnitOfWorkTransaction
    {
        protected IDbContextTransaction DbTransaction { get; private set; }
        public UnitOfWorkTransaction(IDbContextTransaction transaction)
        {
            DbTransaction = transaction;
        }
 
        public void Commit()
        {
            DbTransaction?.Commit();   
        }
        
        public void Rollback()
        {
            DbTransaction?.Rollback();   
        }
 
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbTransaction?.Dispose();
                DbTransaction = null;
            }
        }
 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}