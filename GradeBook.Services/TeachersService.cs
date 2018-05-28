using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.Common.Exceptions;
using GradeBook.Common.Security;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW.Abstractions;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;

namespace GradeBook.Services
{
    public sealed class TeachersService : ITeachersService
    {
        private readonly IUnitOfWork<ITeachersRepository> _teachersUnitOfWork;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public TeachersService(IUnitOfWork<ITeachersRepository> teachersUnitOfWork, IAccountService accountService,
            IMapper mapper)
        {
            _teachersUnitOfWork = teachersUnitOfWork;
            _accountService = accountService;
            _mapper = mapper;
        }

        public async Task<TeacherDto> GetTeacherAsync(int id)
        {
            var teacher = await _teachersUnitOfWork.Repository.GetByIdAsync(id).ConfigureAwait(false);

            if (teacher == null)
            {
                return null;
            }

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<IEnumerable<TeacherDto>> GetTeachersAsync()
        {
            var teachers = await _teachersUnitOfWork.Repository.GetAllAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<TeacherDto>>(teachers);
        }

        public async Task<TeacherDto> CreateTeacherAsync(TeacherDto teacher)
        {
            using (var transaction = await _teachersUnitOfWork.BeginTransactionAsync().ConfigureAwait(false))
            {
                teacher.Role = Roles.Teacher;

                var newAcct = await _accountService.CreateAccountAsync(teacher).ConfigureAwait(false);

                var newTeacher = new Teacher
                {
                    Id = newAcct.Id
                };

                _teachersUnitOfWork.Repository.Add(newTeacher);
                await _teachersUnitOfWork.SaveChangesAsync().ConfigureAwait(false);

                transaction.Commit();

                return await GetTeacherAsync(newTeacher.Id).ConfigureAwait(false);
            }
        }

        public async Task UpdateTeacherAsync(TeacherDto teacher)
        {
            await _accountService.UpdateAccountAsync(teacher).ConfigureAwait(false);
        }

        public async Task DeleteTeacherAsync(int teacherId)
        {
            var teacher = await _teachersUnitOfWork.Repository.GetByIdAsync(teacherId).ConfigureAwait(false);

            if (teacher == null)
            {
                throw new ResourceNotFoundException($"Teacher {teacherId} not found");
            }
            
            _teachersUnitOfWork.Repository.Delete(teacher);
            
            await _teachersUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}