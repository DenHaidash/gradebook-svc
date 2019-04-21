using System;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.Common.Exceptions;
using GradeBook.Common.Mailing.Abstractions;
using GradeBook.Common.Security;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW.Abstractions;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using Microsoft.Extensions.Logging;

namespace GradeBook.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly IUnitOfWork<IAccountRepository> _accountUnitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IUnitOfWork<IAccountRepository> accountUnitOfWork, IEmailSender emailSender, 
            IMapper mapper, ILogger<AccountService> logger)
        {
            _accountUnitOfWork = accountUnitOfWork;
            _emailSender = emailSender;
            _mapper = mapper;
            _logger = logger;
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

        public async Task<AccountDto> CreateAccountAsync(AccountDto acct)
        {
            var salt = PasswordProtector.GenerateSalt();
            var randPassword = PasswordProtector.GenerateSalt(10);

            var newAcct = _mapper.Map<Account>(acct);
            newAcct.PasswordSalt = salt;
            newAcct.PasswordHash = PasswordProtector.SaltString(salt, randPassword);
            newAcct.CreatedAt = DateTime.Now;
            newAcct.UpdatedAt = DateTime.Now;
            
            _accountUnitOfWork.Repository.Add(newAcct);

            await _accountUnitOfWork.SaveChangesAsync().ConfigureAwait(false);

            Task.Run(async () =>
            {
                try
                {
                    await _emailSender
                        .SendEmailAsync(acct.Email, "GradeBook account created", $"Account password: {randPassword}")
                        .ConfigureAwait(false);
                }
                catch (Exception)
                {
                    _logger.LogError($"Failed to send email with password to user {acct.Email}");
                }
            });

            return await GetAccountAsync(newAcct.Login).ConfigureAwait(false);
        }

        public async Task RemoveAccountAsync(int accountId)
        {
            var acct = await _accountUnitOfWork.Repository.GetByIdAsync(accountId).ConfigureAwait(false);

            if (acct == null)
            {
                throw new ResourceNotFoundException($"Аккаунт {accountId} не знайдено");
            }
            
            _accountUnitOfWork.Repository.Delete(acct);

            await _accountUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAccountAsync(AccountDto acct)
        {
            var acctToUpdate = await _accountUnitOfWork.Repository.GetByIdAsync(acct.Id).ConfigureAwait(false);

            if (acctToUpdate == null)
            {
                throw new ResourceNotFoundException($"Аккаунт {acct.Id} не знайдено");
            }

            acctToUpdate.FirstName = acct.FirstName;
            acctToUpdate.LastName = acct.LastName;
            acctToUpdate.MiddleName = acct.MiddleName;
            acctToUpdate.UpdatedAt = DateTime.Now;

            await _accountUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task ChangePasswordAsync(int accountId, string newPassword)
        {
            var acctToUpdate = await _accountUnitOfWork.Repository.GetByIdAsync(accountId).ConfigureAwait(false);

            if (acctToUpdate == null)
            {
                throw new ResourceNotFoundException($"Аккаунт {accountId} не знайдено");
            }
            
            var salt = PasswordProtector.GenerateSalt();
            
            acctToUpdate.PasswordSalt = salt;
            acctToUpdate.PasswordHash = PasswordProtector.SaltString(salt, newPassword);
            acctToUpdate.UpdatedAt = DateTime.Now;

            await _accountUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task ResetPasswordAsync(string login)
        {
            var acctToUpdate = await _accountUnitOfWork.Repository.GetByLoginAsync(login).ConfigureAwait(false);

            if (acctToUpdate == null)
            {
                throw new ResourceNotFoundException($"Аккаунт {login} не знайдено");
            }
            
            var salt = PasswordProtector.GenerateSalt();
            var newPassword = PasswordProtector.GenerateSalt(10);
            
            acctToUpdate.PasswordSalt = salt;
            acctToUpdate.PasswordHash = PasswordProtector.SaltString(salt, newPassword);
            acctToUpdate.UpdatedAt = DateTime.Now;

            await _accountUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
            
            Task.Run(async () =>
            {
                try
                {
                    await _emailSender
                        .SendEmailAsync(acctToUpdate.Login, "GradeBook password reset", $"New password: {newPassword}")
                        .ConfigureAwait(false);
                }
                catch (Exception)
                {
                    _logger.LogError($"Failed to send email with password to user {login}");
                }
            });
        }
    }
}