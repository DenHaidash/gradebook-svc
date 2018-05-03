using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IGradebooksService
    {
        Task<GradebookDto> GetGradebookAsync(int semesterId, int subjectId);
        Task<GradebookDto> GetGradebookAsync(int year, int semester, int subjectId);
        Task<int> CreateGradebookAsync(GradebookDto gradebook);
        Task RemoveGradebookAsync(int gradebookId);
    }
}