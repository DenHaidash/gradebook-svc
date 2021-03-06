﻿using System.Collections.Generic;
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
    public sealed class SubjectsService : ISubjectsService
    {
        private readonly IUnitOfWork<ISubjectsRepository>  _subjectsUnitOfWork;
        private readonly IMapper _mapper;

        public SubjectsService(IUnitOfWork<ISubjectsRepository> subjectsUnitOfWork, IMapper mapper)
        {
            _subjectsUnitOfWork = subjectsUnitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<SubjectDto>> GetSubjectsAsync()
        {
            var subjects = await _subjectsUnitOfWork.Repository.GetAllAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<SubjectDto>>(subjects);
        }

        public async Task<SubjectDto> GetSubjectAsync(int subjectId)
        {
            var subject = await _subjectsUnitOfWork.Repository.GetByIdAsync(subjectId).ConfigureAwait(false);

            if (subject == null)
            {
                return null;
            }

            return _mapper.Map<SubjectDto>(subject);
        }

        public async Task<SubjectDto> CreateSubjectAsync(SubjectDto subject)
        {
            var newSubject = _mapper.Map<Subject>(subject);
            
            _subjectsUnitOfWork.Repository.Add(newSubject);

            await _subjectsUnitOfWork.SaveChangesAsync().ConfigureAwait(false);

            return await GetSubjectAsync(newSubject.Id).ConfigureAwait(false);
        }

        public async Task UpdateSubjectAsync(SubjectDto subject)
        {
            var subjectToUpdate = await _subjectsUnitOfWork.Repository.GetByIdAsync(subject.Id).ConfigureAwait(false);

            if (subjectToUpdate == null)
            {
                throw new ResourceNotFoundException($"Предмет {subject.Id} не знайдений");
            }

            subjectToUpdate.Name = subject.Name;
            
            await _subjectsUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteSubjectAsync(int subjectId)
        {
            var subjectToUpdate = await _subjectsUnitOfWork.Repository.GetByIdAsync(subjectId).ConfigureAwait(false);

            if (subjectToUpdate == null)
            {
                throw new ResourceNotFoundException($"Предмет {subjectId} не знайдений");
            }

            _subjectsUnitOfWork.Repository.Delete(subjectToUpdate);
            
            await _subjectsUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}