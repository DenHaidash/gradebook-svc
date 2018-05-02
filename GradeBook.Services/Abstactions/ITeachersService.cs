using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface ITeachersService
    {
        Task<TeacherDto> GetTeacherAsync(int id);
        Task<IEnumerable<TeacherDto>> GetTeachersAsync();
        Task<int> CreateTeacherAsync(TeacherDto teacher);
        Task UpdateTeacherAsync(TeacherDto teacher);
        Task DeleteTeacherAsync(int teacherId);
    }
}
