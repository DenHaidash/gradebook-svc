using System;

namespace GradeBook.DAL.UoW.Abstractions
{
    public interface IUnitOfWorkTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}