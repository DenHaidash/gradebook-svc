using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;

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

        public async Task<int> CreateTeacherAsync(TeacherDto teacher)
        {
            using (var transaction = await _teachersUnitOfWork.BeginTransactionAsync().ConfigureAwait(false))
            {
                teacher.Role = "teacher";
                
                var newAcctId = await _accountService.CreateAccountAsync(teacher).ConfigureAwait(false);

                var newTeacher = new Teacher
                {
                    Id = newAcctId
                };
            
                _teachersUnitOfWork.Repository.Add(newTeacher);
                await _teachersUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
                
               transaction.Commit();

               return newAcctId;
            }
        }

        public async Task UpdateTeacherAsync(TeacherDto teacher)
        {
            await _accountService.UpdateAccountAsync(teacher).ConfigureAwait(false);
            
            await _teachersUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteTeacherAsync(int teacherId)
        {
            var teacherToUpdate = await _teachersUnitOfWork.Repository.GetByIdAsync(teacherId).ConfigureAwait(false);

            using (var transaction = await _teachersUnitOfWork.BeginTransactionAsync().ConfigureAwait(false))
            {
                await _accountService.DisableAccountAsync(teacherId).ConfigureAwait(false);
                
                teacherToUpdate.IsDeleted = true;
                await _teachersUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
                
                transaction.Commit();
            }
        }
    }
}