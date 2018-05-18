using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.Common.Exceptions;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW.Abstractions;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;

namespace GradeBook.Services
{
    public sealed class GradebookService : IGradebooksService
    {
        private readonly IUnitOfWork<IGradebooksRepository> _gradebookUnitOfWork;
        private readonly IMapper _mapper;

        public GradebookService(IUnitOfWork<IGradebooksRepository> gradebookUnitOfWork, IMapper mapper)
        {
            _gradebookUnitOfWork = gradebookUnitOfWork;
            _mapper = mapper;
        }

        public async Task<GradebookDto> GetGradebookAsync(int semesterId, int subjectId)
        {
            var gradebook = (await _gradebookUnitOfWork.Repository
                .GetAllAsync(s => s.SemesterRefId == semesterId 
                                  && s.SubjectRefId == subjectId)
                .ConfigureAwait(false)).FirstOrDefault();

            return _mapper.Map<GradebookDto>(gradebook);
        }

        public async Task<GradebookDto> GetGradebookAsync(int year, int semester, int subjectId)
        {
            var gradebook = (await _gradebookUnitOfWork.Repository
                .GetAllAsync(s => s.Semester.StartsAt.Year == (semester == 2 ? year + 1 : year)
                                  && s.Semester.SemesterNumber == semester
                                  && s.SubjectRefId == subjectId)
                .ConfigureAwait(false)).FirstOrDefault();

            return _mapper.Map<GradebookDto>(gradebook);
        }

        public async Task<GradebookDto> GetGradebookByGroupAsync(int groupId, int subjectId)
        {
            var gradebook = (await _gradebookUnitOfWork.Repository
                .GetAllAsync(s => s.Semester.GroupRefId == groupId 
                                  && s.SubjectRefId == subjectId)
                .ConfigureAwait(false)).FirstOrDefault();

            if (gradebook == null)
            {
                return null;
            }

            return _mapper.Map<GradebookDto>(gradebook);
        }

        public async Task<GradebookDto> GetGradebookAsync(int gradebookId)
        {
            var gradebook = await _gradebookUnitOfWork.Repository.GetByIdAsync(gradebookId).ConfigureAwait(false);

            if (gradebook == null)
            {
                return null;
            }

            return _mapper.Map<GradebookDto>(gradebook);
        }

        public async Task<GradebookDto> CreateGradebookAsync(GradebookDto gradebook)
        {
            var newGradebook = _mapper.Map<Gradebook>(gradebook);
            
            _gradebookUnitOfWork.Repository.Add(newGradebook);

            await _gradebookUnitOfWork.SaveChangesAsync().ConfigureAwait(false);

            return await GetGradebookAsync(newGradebook.Id).ConfigureAwait(false);
        }

        public async Task RemoveGradebookAsync(int gradebookId)
        {
            var gradebook = await _gradebookUnitOfWork.Repository.GetByIdAsync(gradebookId).ConfigureAwait(false);

            if (gradebook == null)
            {
                throw new ResourceNotFoundException($"Gradebook {gradebookId} not found");
            }
            
            _gradebookUnitOfWork.Repository.Delete(gradebook);

            await _gradebookUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}