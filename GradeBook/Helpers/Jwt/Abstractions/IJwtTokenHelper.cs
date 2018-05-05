using GradeBook.DTO;

namespace GradeBook.Helpers.Jwt.Abstractions
{
    public interface IJwtTokenHelper
    {
        string BuildJwtToken(AccountDto acct);
    }
}