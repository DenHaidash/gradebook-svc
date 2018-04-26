using System.Threading.Tasks;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByLoginAsync(string login);
    }
}