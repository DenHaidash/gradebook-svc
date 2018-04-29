using System;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.Common.Mailing;
using GradeBook.Common.Security;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.DAL.UoW.Base;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Interfaces;

namespace GradeBook.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork<IAccountRepository> _accountUnitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork<IAccountRepository> accountUnitOfWork, IEmailSender emailSender, IMapper mapper)
        {
            _accountUnitOfWork = accountUnitOfWork;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        public async Task<AccountDto> GetAccountAsync(string login)
        {
            var acct = await _accountUnitOfWork.Repository.GetByLoginAsync(login);

            if (acct == null)
            {
                return null;
            }

            return _mapper.Map<AccountDto>(acct);
        }

        public async Task<bool> VerifyPasswordAsync(string login, string password)
        {
            var account = await _accountUnitOfWork.Repository.GetByLoginAsync(login);

            if (account == null)
            {
                return false;
            }

            var saltedPassword = PasswordProtector.SaltString(account.PasswordSalt, password);

            return saltedPassword.Equals(account.PasswordHash);
        }

        public async Task<int> CreateAccountAsync(AccountDto acct)
        {
            var salt = PasswordProtector.GenerateSalt();
            var randPassword = PasswordProtector.GenerateSalt(10);

            var newAcct = _mapper.Map<Account>(acct);
            newAcct.IsActive = true;
            newAcct.PasswordSalt = salt;
            newAcct.PasswordHash = PasswordProtector.SaltString(salt, randPassword);
            newAcct.CreatedAt = DateTime.Now;
            newAcct.UpdatedAt = DateTime.Now;
            
            _accountUnitOfWork.Repository.Add(newAcct);

            await _accountUnitOfWork.SaveAsync().ConfigureAwait(false);

            await _emailSender
                .SendEmailAsync(acct.Email, "GradeBook account created", $"Account password: {randPassword}")
                .ConfigureAwait(false);

            return newAcct.Id;
        }

        public async Task DisableAccountAsync(int accountId)
        {
            var acct = await _accountUnitOfWork.Repository.GetByIdAsync(accountId).ConfigureAwait(false);

            if (acct == null)
            {
                return; // throw
            }

            acct.IsActive = false;

            await _accountUnitOfWork.SaveAsync().ConfigureAwait(false);
        }

        public async Task UpdateAccountAsync(AccountDto acct)
        {
            var acctToUpdate = await _accountUnitOfWork.Repository.GetByIdAsync(acct.Id).ConfigureAwait(false);

            if (acctToUpdate == null)
            {
                // throw

                return;
            }

            acctToUpdate.FirstName = acct.FirstName;
            acctToUpdate.LastName = acct.LastName;
            acctToUpdate.MiddleName = acct.MiddleName;
            acctToUpdate.UpdatedAt = DateTime.Now;

            await _accountUnitOfWork.SaveAsync().ConfigureAwait(false);
        }

        public async Task ChangePasswordAsync(int accountId, string newPassword)
        {
            var acctToUpdate = await _accountUnitOfWork.Repository.GetByIdAsync(accountId).ConfigureAwait(false);

            if (acctToUpdate == null)
            {
                // throw

                return;
            }
            
            var salt = PasswordProtector.GenerateSalt();
            
            acctToUpdate.PasswordSalt = salt;
            acctToUpdate.PasswordHash = PasswordProtector.SaltString(salt, newPassword);
            acctToUpdate.UpdatedAt = DateTime.Now;

            await _accountUnitOfWork.SaveAsync().ConfigureAwait(false);
        }

        public async Task ResetPasswordAsync(string login)
        {
            var acctToUpdate = await _accountUnitOfWork.Repository.GetByLoginAsync(login).ConfigureAwait(false);

            if (acctToUpdate == null)
            {
                // throw

                return;
            }
            
            var salt = PasswordProtector.GenerateSalt();
            var newPassword = PasswordProtector.GenerateSalt(10);
            
            acctToUpdate.PasswordSalt = salt;
            acctToUpdate.PasswordHash = PasswordProtector.SaltString(salt, newPassword);
            acctToUpdate.UpdatedAt = DateTime.Now;

            await _accountUnitOfWork.SaveAsync().ConfigureAwait(false);
            
            await _emailSender
                .SendEmailAsync(acctToUpdate.Login, "GradeBook password reset", $"New password: {newPassword}")
                .ConfigureAwait(false);
        }
    }
}