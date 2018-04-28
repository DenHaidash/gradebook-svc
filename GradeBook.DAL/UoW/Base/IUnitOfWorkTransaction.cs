using System;

namespace GradeBook.DAL.UoW.Base
{
    public interface IUnitOfWorkTransaction : IDisposable
    {
        void Commit();
        void Rollback();
    }
}