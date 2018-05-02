using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IGroupStudentsService
    {
        Task<IEnumerable<StudentDto>> GetStudentsAsync(int groupId);
    }
}