using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DAL.Repositories.Abstractions;
using GradeBook.DAL.UoW.Base;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Services
{
    // todo: think about abstact CrudService<TRepository>
    public class SpecialitiesService : ISpecialitiesService
    {
        private readonly IUnitOfWork<ISpecialitiesRepository> _specialitiesUnitOfWork;
        private readonly IMapper _mapper;

        public SpecialitiesService(IUnitOfWork<ISpecialitiesRepository> specialitiesUnitOfWork, IMapper mapper)
        {
            _specialitiesUnitOfWork = specialitiesUnitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<SpecialtyDto>> GetSpecialitiesAsync()
        {
            var specialities = await _specialitiesUnitOfWork.Repository.GetAllAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<SpecialtyDto>>(specialities);
        }

        public async Task<SpecialtyDto> GetSpecialityAsync(int specialtyId)
        {
            var specialty = await _specialitiesUnitOfWork.Repository.GetByIdAsync(specialtyId).ConfigureAwait(false);

            if (specialty == null)
            {
                return null;
            }
            
            return _mapper.Map<SpecialtyDto>(specialty);
        }

        public async Task<int> CreateSpecialityAsync(SpecialtyDto specialty)
        {
            var newSpeciality = _mapper.Map<Specialty>(specialty);
            
            _specialitiesUnitOfWork.Repository.Add(newSpeciality);

            await _specialitiesUnitOfWork.SaveAsync().ConfigureAwait(false);

            return newSpeciality.Id;
        }

        public async Task UpdateSpecialityAsync(SpecialtyDto specialty)
        {
            var specialityToUpdate = await _specialitiesUnitOfWork.Repository.GetByIdAsync(specialty.Id).ConfigureAwait(false);

            if (specialityToUpdate == null)
            {
                return;
            }
            
            specialityToUpdate.Name = specialty.Name;
            specialityToUpdate.Code = specialty.Code;

            await _specialitiesUnitOfWork.SaveAsync().ConfigureAwait(false);
        }

        public async Task DeleteSpecialityAsync(int specialityId)
        {
            var speciality = await _specialitiesUnitOfWork.Repository.GetByIdAsync(specialityId).ConfigureAwait(false);

            if (speciality == null)
            {
                return;
            }
            
            _specialitiesUnitOfWork.Repository.Delete(speciality);

            await _specialitiesUnitOfWork.SaveAsync().ConfigureAwait(false);
        }
    }
}