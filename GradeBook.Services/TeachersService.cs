using System;
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
    public class TeachersService : ITeachersService
    {
        private readonly ITeachersRepository _teachersRepository;
        private readonly IAccountService _accountService;

        public TeachersService(ITeachersRepository teachersRepository, IAccountService accountService)
        {
            _teachersRepository = teachersRepository;
            _accountService = accountService;
        }
        
        public async Task<TeacherDto> GetTeacherAsync(int id)
        {
            var teacher = await _teachersRepository.GetByIdAsync(id); // todo include specializations, acct,

            if (teacher == null)
            {
                return null;
            }
            
            return new TeacherDto()
            {
                Id = teacher.Id,
                FirstName = teacher.Account.FirstName,
                LastName = teacher.Account.LastName,
                MiddleName = teacher.Account.MiddleName,
                Specializations = teacher.Specializations.Select(s => new SubjectDto()
                {
                    Id = s.SubjectRefId,
                    Name = s.Subject.Name
                })
            };
        }

        public async Task<IEnumerable<TeacherDto>> GetTeachersWithSpecialityAsync(int subjectId)
        {
            var teachers = await _teachersRepository.GetAll()
                .Where(t => t.Specializations.Any(s => s.SubjectRefId == subjectId)).ToListAsync();

            return teachers.Select(t => new TeacherDto()
            {
                Id = t.Id,
                FirstName = t.Account.FirstName,
                LastName = t.Account.LastName,
                MiddleName = t.Account.MiddleName,
                Specializations = t.Specializations.Select(s => new SubjectDto()
                {
                    Id = s.SubjectRefId,
                    Name = s.Subject.Name
                })
            });
        }

        public async Task<IEnumerable<TeacherDto>> GetTeachersAsync()
        {
            var teachers = await _teachersRepository.GetAll().ToListAsync();

            return teachers.Select(t => new TeacherDto()
            {
                Id = t.Id,
                FirstName = t.Account.FirstName,
                LastName = t.Account.LastName,
                MiddleName = t.Account.MiddleName,
                Specializations = t.Specializations.Select(s => new SubjectDto()
                {
                    Id = s.SubjectRefId,
                    Name = s.Subject.Name
                })
            });
        }

        public async Task<int> CreateTeacherAsync(TeacherDto teacher)
        {
            var newAcctId = await _accountService.CreateAccountAsync(teacher);

            var newTeacher = new Teacher()
            {
                Id = newAcctId,
                Specializations = teacher.Specializations.Select(s => new TeacherSubject()
                {
                    // todo map teacher id as acct id
                    SubjectRefId = s.Id
                })
            };
            
            _teachersRepository.Add(newTeacher);
            //save

            return newAcctId;
        }

        public async Task UpdateTeacherAsync(TeacherDto teacher)
        {
            var teacherToUpdate = await _teachersRepository.GetByIdAsync(teacher.Id); // todo: eager load group and acct

            teacherToUpdate.Account.FirstName = teacher.FirstName;
            teacherToUpdate.Account.LastName = teacher.LastName;
            teacherToUpdate.Account.MiddleName = teacher.MiddleName;
            teacherToUpdate.Account.UpdatedAt = DateTime.Now;
            
            // todo: update teacher specializations
            
            // save
        }

        public async Task DeleteTeacherAsync(int teacherId)
        {
            var teacherToUpdate = await _teachersRepository.GetByIdAsync(teacherId); 

            // in transaction
            
            teacherToUpdate.IsDeleted = true;
            teacherToUpdate.Account.IsActive = false; // use Acct service Disable()
            
            // save
        }
    }
}