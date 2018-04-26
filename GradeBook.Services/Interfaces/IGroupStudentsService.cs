using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Interfaces
{
    public interface IGroupStudentsService
    {
        Task<IEnumerable<StudentDto>> GetStudentsAsync(int groupId);
    }
}