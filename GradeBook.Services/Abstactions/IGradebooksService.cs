using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IGradebooksService
    {
        Task<GradebookDto> GetGradebookAsync(int semesterId, int subjectId);
        Task<GradebookDto> GetGradebookAsync(int year, int semester, int subjectId);
        Task<GradebookDto> GetGradebookAsync(int gradebookId);
        Task<GradebookDto> GetGradebookByGroupAsync(int groupId, int subjectId);
        Task<GradebookDto> CreateGradebookAsync(GradebookDto gradebook);
        Task RemoveGradebookAsync(int gradebookId);
    }
}