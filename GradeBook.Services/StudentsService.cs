using System;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Interfaces;

namespace GradeBook.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IAccountService _acctService;

        public StudentsService(IStudentsRepository studentsRepository, IAccountService acctService)
        {
            _studentsRepository = studentsRepository;
            _acctService = acctService;
        }

        public async Task<StudentDto> GetStudentAsync(int id)
        {
            var student = await _studentsRepository.GetByIdAsync(id);
            
            return new StudentDto()
            {
                FirstName = student.Account.FirstName,
                LastName = student.Account.LastName,
                MiddleName = student.Account.MiddleName,
                Id = student.Id,
                Group = new GroupDto()
                {
                    Code = student.Group.Code,
                    Id = student.Group.Id,
                    Specialty = new SpecialtyDto()
                    {
                        Code = student.Group.Specialty.Code,
                        Id = student.Group.Specialty.Id,
                        Name = student.Group.Specialty.Name
                    }
                }
            };
        }

        public async Task<int> CreateStudentAsync(StudentDto student)
        {
            // todo: in transaction
            
            var newAcctId = await _acctService.CreateAccountAsync(student);

            var newStudent = new Student()
            {
                Id = newAcctId,
                GroupRefId = student.Group.Id
            };
            
            _studentsRepository.Add(newStudent);
            
            // save

            return newAcctId;
        }

        public async Task UpdateStudentAsync(StudentDto student)
        {
            var studentToUpdate = await _studentsRepository.GetByIdAsync(student.Id); // todo: eager load group and acct

            studentToUpdate.GroupRefId = student.Group.Id;
            studentToUpdate.Account.FirstName = student.FirstName;
            studentToUpdate.Account.LastName = student.LastName;
            studentToUpdate.Account.MiddleName = student.MiddleName;
            studentToUpdate.Account.UpdatedAt = DateTime.Now;
            
            //save

        }

        public async Task DeleteStudentAsync(int studentId)
        {
            var studentToUpdate = await _studentsRepository.GetByIdAsync(studentId); // todo: eager load group and acct

            // in transaction
            
            studentToUpdate.IsDeleted = true;
            studentToUpdate.Account.IsActive = false;
            
            // save
        }
    }
}