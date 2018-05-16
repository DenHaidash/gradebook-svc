using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IAccountService
    {
        Task<AccountDto> GetAccountAsync(string login);
        Task<bool> VerifyPasswordAsync(string login, string password);
        Task<AccountDto> CreateAccountAsync(AccountDto acct);
        Task DisableAccountAsync(int accountId);
        Task EnableAccountAsync(int accountId);
        Task UpdateAccountAsync(AccountDto acct);
        Task ChangePasswordAsync(int accountId, string newPassword);
        Task ResetPasswordAsync(string email);
    }
}