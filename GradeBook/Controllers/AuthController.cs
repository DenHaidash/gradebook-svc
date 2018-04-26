using System;
using System.Threading.Tasks;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;

namespace GradeBook.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        
        [HttpPost]
        public async Task<IActionResult> GetAuthToken(string login, string password)
        {
            var isLoginValid = await _accountService.VerifyPasswordAsync(login, password);
            
            return Ok(isLoginValid);
        }
        
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(string oldPassword, string newPassword)
        {
            //var isLoginValid = await _accountService.VerifyPasswordAsync(login, password);
            
            return Ok();
        }
        
        [HttpPost("restore-password")]
        public async Task<IActionResult> RestorePassword(string email)
        {
            //var isLoginValid = await _accountService.VerifyPasswordAsync(login, password);
            
            return Ok();
        }
        
        [HttpPost("save-password")]
        public async Task<IActionResult> SaveRestoredPassword(string token, string password)
        {
            //var isLoginValid = await _accountService.VerifyPasswordAsync(login, password);
            
            return Ok();
        }
    }
}