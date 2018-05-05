using System;

namespace GradeBook.DAL.UoW
{
    public interface IUnitOfWorkTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}