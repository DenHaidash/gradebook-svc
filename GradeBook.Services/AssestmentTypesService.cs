using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DTO;
using GradeBook.Services.Abstactions;

namespace GradeBook.Services
{
    public sealed class AssestmentTypesService : IAssestmentTypesService
    {
        private readonly IAssestmentTypesRepository _assestmentTypesRepository;
        private readonly IMapper _mapper;

        public AssestmentTypesService(IAssestmentTypesRepository assestmentTypesRepository, IMapper mapper)
        {
            _assestmentTypesRepository = assestmentTypesRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<AssessmentTypeDto>> GetAssestmentTypesAsync()
        {
            var assestmentTypes = await _assestmentTypesRepository.GetAllAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<AssessmentTypeDto>>(assestmentTypes);
        }
    }
}