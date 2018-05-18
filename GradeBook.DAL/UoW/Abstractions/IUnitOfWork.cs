using System;
using System.Data;
using System.Threading.Tasks;

namespace GradeBook.DAL.UoW.Abstractions
{
        public interface IUnitOfWork<out TRepository> : IDisposable
        {
                TRepository Repository { get; }
                Task<IUnitOfWorkTransaction> BeginTransactionAsync(IsolationLevel level = IsolationLevel.ReadCommitted);
                Task SaveChangesAsync();
        }
}