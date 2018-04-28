using System;
using System.Data;
using System.Threading.Tasks;

namespace GradeBook.DAL.UoW.Base
{
        public interface IUnitOfWork<TRepository> : IDisposable
        {
                TRepository Repository { get; }
                Task<IUnitOfWorkTransaction> InTransactionAsync(IsolationLevel level);
                Task SaveAsync();
        }
}