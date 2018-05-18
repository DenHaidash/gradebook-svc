using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Helpers.Jwt.Abstractions;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using GradeBook.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Produces("application/json")]
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
        
        /// <summary>
        /// Get auth token
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAuthTokenAsync([FromBody]LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }
            
            if (!(await _accountService.VerifyPasswordAsync(loginModel.Login, loginModel.Password)))
            {
                return Unauthorized();
            }

            var user = await _accountService.GetAccountAsync(loginModel.Login);

            return Ok(new TokenDto { Token = _jwtTokenHelper.BuildJwtToken(user) });
        }
        
        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="changePasswordModel"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ChangePasswordAsync([FromBody]ChangePasswordViewModel changePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
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
        
        /// <summary>
        /// Reset password
        /// </summary>
        [AllowAnonymous]
        [HttpPost("reset-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RestorePasswordAsync([FromBody]PasswordResetViewModel passwordResetModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }
            
            await _accountService.ResetPasswordAsync(passwordResetModel.Email);
            
            return NoContent();
        }
    }
}