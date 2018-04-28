using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GradeBook.Common.Mailing;
using GradeBook.DTO;
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
        private readonly IConfiguration _config;

        public AuthController(IAccountService accountService, IConfiguration config)
        {
            _accountService = accountService;
            _config = config;
        }
        
        [HttpPost]
        public async Task<IActionResult> GetAuthToken([FromBody]LoginViewModel loginModel)
        {
            if (loginModel == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var isLoginValid = await _accountService.VerifyPasswordAsync(loginModel.Login, loginModel.Password);

            if (!isLoginValid)
            {
                return Unauthorized();
            }

            var user = await _accountService.GetAccountAsync(loginModel.Login);

            return Ok(_buildToken(user));
        }
        
        // todo: extract to helper
        private string _buildToken(AccountDto user)
        {
            var claims = new[] {
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("MiddleName", user.MiddleName),
                new Claim("Role", user.Role),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Sid, user.Id.ToString()), 
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordViewModel changePasswordModel)
        {
            if (changePasswordModel == null || !ModelState.IsValid)
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
        
        [HttpPost("restore-password")]
        public async Task<IActionResult> RestorePassword(string email)
        {
            //var isLoginValid = await _accountService.VerifyPasswordAsync(login, password);
            
            return Ok();
        }
    }
}