using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GradeBook.DTO;
using GradeBook.Helpers.Jwt.Abstractions;
using GradeBook.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace GradeBook.Helpers.Jwt
{
    public class JwtTokenHelper : IJwtTokenHelper
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenHelper(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        private static IEnumerable<Claim> _createClaims(AccountDto acct)
        {
            return new[]
            {
                new Claim("FirstName", acct.FirstName),
                new Claim("LastName", acct.LastName),
                new Claim("MiddleName", acct.MiddleName),
                new Claim("Role", acct.Role),
                new Claim("Email", acct.Email),
                new Claim("Id", acct.Id.ToString()),
                
                new Claim(ClaimTypes.Role, acct.Role),
                new Claim(ClaimTypes.Sid, acct.Id.ToString()),
                new Claim(ClaimTypes.Email, acct.Email)
            };
        }
        
        public string BuildJwtToken(AccountDto acct)
        {
            var claims = _createClaims(acct);
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_jwtOptions.Issuer,
                _jwtOptions.Issuer,
                claims,
                expires: DateTime.Now.AddMinutes(_jwtOptions.TokenLifetimeMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}