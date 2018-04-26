using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDto> GetAccountAsync(string login);
        Task<bool> VerifyPasswordAsync(string login, string password);
        Task<int> CreateAccountAsync(AccountDto acct);
        Task DisableAccountAsync(int accountId);
        Task UpdateAccountAsync(AccountDto acct);
        Task ChangePasswordAsync(int accountId, string newPassword);
    }
}