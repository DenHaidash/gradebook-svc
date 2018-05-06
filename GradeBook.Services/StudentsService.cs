using System.Threading.Tasks;
using AutoMapper;
using GradeBook.Common.Exceptions;
using GradeBook.Common.Security;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW;
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

            if (student == null || student.IsDeleted)
            {
                return null;
            }
            
            return _mapper.Map<StudentDto>(student);
        }

        public async Task<int> CreateStudentAsync(StudentDto student)
        {
            using (var tranaction = await _studentsUnitOfWork.BeginTransactionAsync().ConfigureAwait(false))
            {
                student.Role = Roles.Student;
                
                var newAcctId = await _acctService.CreateAccountAsync(student).ConfigureAwait(false);

                var newStudent = new Student
                {
                    Id = newAcctId,
                    GroupRefId = student.Group.Id
                };
            
                _studentsUnitOfWork.Repository.Add(newStudent);
                await _studentsUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
                
                tranaction.Commit();

                return newAcctId;
            }
        }

        public async Task UpdateStudentAsync(StudentDto student)
        {
            await _acctService.UpdateAccountAsync(student).ConfigureAwait(false);
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var studentToUpdate = await _studentsUnitOfWork.Repository.GetByIdAsync(studentId).ConfigureAwait(false);

            if (studentToUpdate == null || studentToUpdate.IsDeleted)
            {
                throw new ResourceNotFoundException($"Student {studentId} not found");
            }
            
            using (var tranaction = await _studentsUnitOfWork.BeginTransactionAsync().ConfigureAwait(false))
            {
                await _acctService.DisableAccountAsync(studentId).ConfigureAwait(false);
                
                studentToUpdate.IsDeleted = true;

                await _studentsUnitOfWork.SaveChangesAsync().ConfigureAwait(false);
                
                tranaction.Commit();
            }
        }
    }
}