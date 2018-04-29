using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GradeBook.Common.Mailing;
using GradeBook.DTO;
using GradeBook.Helpers;
using GradeBook.Models;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GradeBook.Controllers
{
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public AuthController(IAccountService accountService, IJwtTokenHelper jwtTokenHelper)
        {
            _accountService = accountService;
            _jwtTokenHelper = jwtTokenHelper;
        }
        
        [HttpPost]
        public async Task<IActionResult> GetAuthTokenAsync([FromBody]LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            if (!(await _accountService.VerifyPasswordAsync(loginModel.Login, loginModel.Password)))
            {
                return Unauthorized();
            }

            var user = await _accountService.GetAccountAsync(loginModel.Login);

            return Ok(_jwtTokenHelper.BuildJwtToken(user));
        }
        
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody]ChangePasswordViewModel changePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var email = User.Claims.First(c => c.Type == ClaimTypes.Email).Value;
            
            var isLoginValid = await _accountService.VerifyPasswordAsync(email, changePasswordModel.OldPassword);

            if (!isLoginValid)
            {
                return Unauthorized();
            }
            
            var acctId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);

            await _accountService.ChangePasswordAsync(acctId, changePasswordModel.NewPassword);
            
            return NoContent();
        }
        
        [HttpPost("reset-password")]
        public async Task<IActionResult> RestorePasswordAsync([FromBody]PasswordResetViewModel passwordResetModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            await _accountService.ResetPasswordAsync(passwordResetModel.Email);
            
            return NoContent();
        }
    }
}