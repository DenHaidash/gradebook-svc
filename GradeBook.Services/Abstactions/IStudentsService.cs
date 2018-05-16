using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IStudentsService
    {
        Task<StudentDto> GetStudentAsync(int id);
        Task<StudentDto> CreateStudentAsync(StudentDto student);
        Task UpdateStudentAsync(StudentDto student);
        Task DeleteStudentAsync(int studentId);
    }
}