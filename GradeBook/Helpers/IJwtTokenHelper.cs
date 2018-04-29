using GradeBook.DTO;

namespace GradeBook.Helpers
{
    public interface IJwtTokenHelper
    {
        string BuildJwtToken(AccountDto acct);
    }
}