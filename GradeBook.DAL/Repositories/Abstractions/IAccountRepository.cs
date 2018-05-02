using System.Threading.Tasks;
using GradeBook.Models;

namespace GradeBook.DAL.Repositories.Abstractions
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByLoginAsync(string login);
    }
}