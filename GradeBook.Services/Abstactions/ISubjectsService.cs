using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface ISubjectsService
    {
        Task<IEnumerable<SubjectDto>> GetSubjectsAsync();
        Task<SubjectDto> GetSubjectAsync(int subjectId);
        Task<SubjectDto> CreateSubjectAsync(SubjectDto subject);
        Task UpdateSubjectAsync(SubjectDto subject);
        Task DeleteSubjectAsync(int subjectId);
    }
}