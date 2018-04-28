using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.DAL.UoW.Base;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Services
{
    public class TeachersService : ITeachersService
    {
        private readonly IUnitOfWork<ITeachersRepository> _teachersUnitOfWork;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public TeachersService(IUnitOfWork<ITeachersRepository> teachersUnitOfWork, IAccountService accountService, IMapper mapper)
        {
            _teachersUnitOfWork = teachersUnitOfWork;
            _accountService = accountService;
            _mapper = mapper;
        }
        
        public async Task<TeacherDto> GetTeacherAsync(int id)
        {
            // todo include specializations, acct,
            var teacher = await _teachersUnitOfWork.Repository.GetByIdAsync(id).ConfigureAwait(false);

            if (teacher == null)
            {
                return null;
            }
            
            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<IEnumerable<TeacherDto>> GetTeachersWithSpecialityAsync(int subjectId)
        {
            var teachers = await _teachersUnitOfWork.Repository
                .GetAllAsync(t => t.Specializations.Any(s => s.SubjectRefId == subjectId))
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task<IEnumerable<TeacherDto>> GetTeachersAsync()
        {
            var teachers = await _teachersUnitOfWork.Repository.GetAllAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task<int> CreateTeacherAsync(TeacherDto teacher)
        {
            using (var transaction = await _teachersUnitOfWork.InTransactionAsync(IsolationLevel.ReadCommitted))
            {
                teacher.Role = "teacher";
                
                var newAcctId = await _accountService.CreateAccountAsync(teacher).ConfigureAwait(false);

                var newTeacher = new Teacher()
                {
                    Id = newAcctId,
//                    Specializations = teacher.Specializations.Select(s => new TeacherSubject()
//                    {
//                        // todo map teacher id as acct id
//                        SubjectRefId = s.Id
//                    })
                };
            
                _teachersUnitOfWork.Repository.Add(newTeacher);
                await _teachersUnitOfWork.SaveAsync().ConfigureAwait(false);
                
               transaction.Commit();

               return newAcctId;
            }
        }

        public async Task UpdateTeacherAsync(TeacherDto teacher)
        {
            var teacherToUpdate = await _teachersUnitOfWork.Repository.GetByIdAsync(teacher.Id);

            teacherToUpdate.Account.FirstName = teacher.FirstName;
            teacherToUpdate.Account.LastName = teacher.LastName;
            teacherToUpdate.Account.MiddleName = teacher.MiddleName;
            teacherToUpdate.Account.UpdatedAt = DateTime.Now;
            
            // todo: update teacher specializations

            await _teachersUnitOfWork.SaveAsync().ConfigureAwait(false);
        }

        public async Task DeleteTeacherAsync(int teacherId)
        {
            var teacherToUpdate = await _teachersUnitOfWork.Repository.GetByIdAsync(teacherId);

            using (var transaction = await _teachersUnitOfWork.InTransactionAsync(IsolationLevel.ReadCommitted))
            {
                await _accountService.DisableAccountAsync(teacherId).ConfigureAwait(false);
                
                teacherToUpdate.IsDeleted = true;
                await _teachersUnitOfWork.SaveAsync().ConfigureAwait(false);
                
                transaction.Commit();
            }
        }
    }
}