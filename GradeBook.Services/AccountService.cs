using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Interfaces;

namespace GradeBook.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<AccountDto> GetAccountAsync(string login)
        {
            var acct = await _accountRepository.GetByLoginAsync(login);

            if (acct == null)
            {
                return null;
            }

            return new AccountDto()
            {
                Id = acct.Id,
                FirstName = acct.FirstName,
                LastName = acct.LastName,
                MiddleName = acct.MiddleName,
                Role = acct.Role
            };
        }

        public async Task<bool> VerifyPasswordAsync(string login, string password)
        {
            var account = await _accountRepository.GetByLoginAsync(login);

            if (account == null)
            {
                return false;
            }

            if (!account.IsActive)
            {
                throw new Exception("Acct is disabled"); // todo: custom exception
            }

            var saltedPassword =
                new SHA256Managed().ComputeHash(
                    Encoding.Unicode.GetBytes(account.PasswordSalt + password)).ToString();

            return saltedPassword.Equals(account.PasswordHash);
        }

        public async Task<int> CreateAccountAsync(AccountDto acct)
        {
            var newAcct = new Account()
            {
                FirstName = acct.FirstName,
                LastName = acct.LastName,
                IsActive = true,
                Login = "",
                MiddleName = acct.MiddleName,
                PasswordSalt = "",
                PasswordHash = "",
                Role = acct.Role,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            
            _accountRepository.Add(newAcct);
            
            // save changes

            return newAcct.Id;
        }

        public async Task DisableAccountAsync(int accountId)
        {
            var acct = await _accountRepository.GetByIdAsync(accountId).ConfigureAwait(false);

            if (acct == null)
            {
                return; // throw
            }

            acct.IsActive = false;
            
            // save changes
        }

        public async Task UpdateAccountAsync(AccountDto acct)
        {
            var acctToUpdate = await _accountRepository.GetByIdAsync(acct.Id).ConfigureAwait(false);

            if (acctToUpdate == null)
            {
                // throw

                return;
            }

            acctToUpdate.FirstName = acct.FirstName;
            acctToUpdate.LastName = acct.LastName;
            acctToUpdate.MiddleName = acct.MiddleName;
            acctToUpdate.UpdatedAt = DateTime.Now;
            
            // save
        }

        public async Task ChangePasswordAsync(int accountId, string newPassword)
        {
            var acctToUpdate = await _accountRepository.GetByIdAsync(accountId).ConfigureAwait(false);

            acctToUpdate.PasswordSalt = "";
            acctToUpdate.PasswordHash = "";
            acctToUpdate.UpdatedAt = DateTime.Now;

            // save
        }
    }
}