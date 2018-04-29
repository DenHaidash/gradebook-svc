using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.DAL.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(GradebookContext context) : base(context)
        {
            
        }

        public async Task<Account> GetByLoginAsync(string login)
        {
            return await Set
                .FirstOrDefaultAsync(i => i.Login == login && i.IsActive)
                .ConfigureAwait(false);
        }
    }
}