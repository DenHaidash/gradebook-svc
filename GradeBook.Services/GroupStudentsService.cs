using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DTO;
using GradeBook.Services.Abstactions;

namespace GradeBook.Services
{
    public sealed class GroupStudentsService : IGroupStudentsService
    {
        private readonly IStudentsRepository _studentsRepository;
        private readonly IMapper _mapper;

        public GroupStudentsService(IStudentsRepository studentsRepository, IMapper mapper)
        {
            _studentsRepository = studentsRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<StudentDto>> GetStudentsAsync(int groupId)
        {
            var students = await _studentsRepository
                .GetAllAsync(s => s.GroupRefId == groupId)
                .ConfigureAwait(false);

            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }
    }
}