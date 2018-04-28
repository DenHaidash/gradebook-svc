using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeBook.DAL.Repositories.Interfaces;
using GradeBook.DTO;
using GradeBook.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Services
{
    public class GroupStudentsService : IGroupStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;

        public GroupStudentsService(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }
        
        public async Task<IEnumerable<StudentDto>> GetStudentsAsync(int groupId)
        {
            var students = await _studentsRepository
                .GetAllAsync(s => s.GroupRefId == groupId && !s.IsDeleted)
//                .Include(s => s.Group.Specialty)
//                .Include(s => s.Account) // todo: should be in repo?
                .ConfigureAwait(false);

            return students.Select(s => new StudentDto()
            {
                FirstName = s.Account.FirstName,
                LastName = s.Account.LastName,
                MiddleName = s.Account.MiddleName,
                Id = s.Id,
                Group = new GroupDto()
                {
                    Code = s.Group.Code,
                    Id = s.Group.Id,
                    Specialty = new SpecialtyDto()
                    {
                        Code = s.Group.Specialty.Code,
                        Id = s.Group.Specialty.Id,
                        Name = s.Group.Specialty.Name
                    }
                }
            });
        }
    }
}