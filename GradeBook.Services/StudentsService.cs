using System;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.DAL.UoW.Base;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;

namespace GradeBook.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IUnitOfWork<IStudentsRepository> _studentsUnitOfWork;
        private readonly IAccountService _acctService;
        private readonly IMapper _mapper;

        public StudentsService(IUnitOfWork<IStudentsRepository> studentsUnitOfWork, IAccountService acctService, IMapper mapper)
        {
            _studentsUnitOfWork = studentsUnitOfWork;
            _acctService = acctService;
            _mapper = mapper;
        }

        public async Task<StudentDto> GetStudentAsync(int id)
        {
            var student = await _studentsUnitOfWork.Repository.GetByIdAsync(id).ConfigureAwait(false);

            if (student == null)
            {
                return null;
            }
            
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<int> CreateStudentAsync(StudentDto student)
        {
            using (var tranaction = await _studentsUnitOfWork.InTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false))
            {
                student.Role = "student"; // todo: move to mapper or ctor
                
                var newAcctId = await _acctService.CreateAccountAsync(student).ConfigureAwait(false);

                var newStudent = new Student()
                {
                    Id = newAcctId,
                    GroupRefId = student.Group.Id
                };
            
                _studentsUnitOfWork.Repository.Add(newStudent);
                await _studentsUnitOfWork.SaveAsync().ConfigureAwait(false);
                
                tranaction.Commit();

                return newAcctId;
            }
        }

        public async Task UpdateStudentAsync(StudentDto student)
        {
            await _acctService.UpdateAccountAsync(student).ConfigureAwait(false);

            //studentToUpdate.GroupRefId = student.Group.Id;
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var studentToUpdate = await _studentsUnitOfWork.Repository.GetByIdAsync(studentId).ConfigureAwait(false);

            using (var tranaction = await _studentsUnitOfWork.InTransactionAsync(IsolationLevel.ReadCommitted)
                .ConfigureAwait(false))
            {
                await _acctService.DisableAccountAsync(studentId).ConfigureAwait(false);
                
                studentToUpdate.IsDeleted = true;

                await _studentsUnitOfWork.SaveAsync().ConfigureAwait(false);
                
                tranaction.Commit();
            }
        }
    }
}