using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Services
{
    public class SubjectsService : ISubjectsService
    {
        private readonly ISubjectsRepository _subjectsRepository;

        public SubjectsService(ISubjectsRepository subjectsRepository)
        {
            _subjectsRepository = subjectsRepository;
        }
        
        public async Task<IEnumerable<SubjectDto>> GetSubjectsAsync()
        {
            var subjects = await _subjectsRepository.GetAll().ToListAsync();

            return subjects.Select(s => new SubjectDto()
            {
                Id = s.Id,
                Name = s.Name
            });
        }

        public async Task<SubjectDto> GetSubjectAsync(int subjectId)
        {
            var subject = await _subjectsRepository.GetByIdAsync(subjectId);
            
            return new SubjectDto()
            {
                Id = subject.Id,
                Name = subject.Name
            };
        }

        public async Task<int> CreateSubjectAsync(SubjectDto subject)
        {
            var newSubject = new Subject()
            {
                Name = subject.Name
            };
            
            _subjectsRepository.Add(newSubject);
            
            // save

            return newSubject.Id;
        }

        public async Task UpdateSubjectAsync(SubjectDto subject)
        {
            var subjectToUpdate = await _subjectsRepository.GetByIdAsync(subject.Id);

            if (subjectToUpdate == null)
            {
                return; // throw NotFoundEx
            }

            subjectToUpdate.Name = subject.Name;
            
            // save
        }

        public async Task DeleteSubjectAsync(int subjectId)
        {
            var subjectToUpdate = await _subjectsRepository.GetByIdAsync(subjectId);

            if (subjectToUpdate == null)
            {
                return; // throw NotFoundEx
            }

            _subjectsRepository.Delete(subjectToUpdate);
        }
    }
}