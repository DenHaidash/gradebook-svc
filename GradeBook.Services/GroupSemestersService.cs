﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW.Base;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;

namespace GradeBook.Services
{
    public class GroupSemestersService : IGroupSemestersService
    {
        private readonly IUnitOfWork<ISemestersRepository> _semesterScheduleUnitOfWork;
        private readonly IMapper _mapper;

        public GroupSemestersService(IUnitOfWork<ISemestersRepository> semesterScheduleUnitOfWork, IMapper mapper)
        {
            _semesterScheduleUnitOfWork = semesterScheduleUnitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<SemesterDto>> GetGroupSemestersAsync(int groupId)
        {
            var semesters = await _semesterScheduleUnitOfWork.Repository.GetAllAsync(s => s.GroupRefId == groupId)
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<SemesterDto>>(semesters);
        }

        public async Task<SemesterDto> GetGroupSemesterAsync(int groupId, int year, int semesterNumber)
        {
            var semester = (await _semesterScheduleUnitOfWork.Repository
                    .GetAllAsync(s =>
                        s.GroupRefId == groupId && s.StartsAt.Year == year && s.SemesterNumber == semesterNumber)
                    .ConfigureAwait(false))
                .FirstOrDefault();

            if (semester == null)
            {
                return null;
            }

            return _mapper.Map<SemesterDto>(semester);
        }

        private Semester _addSemesterToRepo(SemesterDto semester)
        {
            var newSemester = _mapper.Map<Semester>(semester);
            
            _semesterScheduleUnitOfWork.Repository.Add(newSemester);

            return newSemester;
        }
        
        public async Task<int> CreateGroupSemesterAsync(SemesterDto semester)
        {
            var newSemester = _addSemesterToRepo(semester);

            await _semesterScheduleUnitOfWork.SaveAsync().ConfigureAwait(false);

            return newSemester.Id;
        }
        
        public async Task CreateGroupSemestersAsync(IEnumerable<SemesterDto> semesters)
        {
            foreach (var semester in semesters)
            {
                _addSemesterToRepo(semester);
            }

            await _semesterScheduleUnitOfWork.SaveAsync().ConfigureAwait(false);
        }

        public async Task DeleteGroupSemester(int semesterId)
        {
            var semesterToDelete = await _semesterScheduleUnitOfWork.Repository.GetByIdAsync(semesterId).ConfigureAwait(false);

            if (semesterToDelete == null)
            {
                return;
            }
            
            _semesterScheduleUnitOfWork.Repository.Delete(semesterToDelete);

            await _semesterScheduleUnitOfWork.SaveAsync().ConfigureAwait(false);
        }
    }
}