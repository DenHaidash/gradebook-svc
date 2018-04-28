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
    public class SpecialitiesService : ISpecialitiesService
    {
        private readonly ISpecialitiesRepository _specialitiesRepository;

        public SpecialitiesService(ISpecialitiesRepository specialitiesRepository)
        {
            _specialitiesRepository = specialitiesRepository;
        }
        
        public async Task<IEnumerable<SpecialtyDto>> GetSpecialitiesAsync()
        {
            var specialities = await _specialitiesRepository.GetAllAsync().ConfigureAwait(false);

            return specialities.Select(s => new SpecialtyDto()
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name
            });
        }

        public async Task<SpecialtyDto> GetSpecialityAsync(int specialtyId)
        {
            var specialty = await _specialitiesRepository.GetByIdAsync(specialtyId);

            return new SpecialtyDto()
            {
                Id = specialty.Id,
                Code = specialty.Code,
                Name = specialty.Name
            };
        }

        public async Task<int> CreateSpecialityAsync(SpecialtyDto specialty)
        {
            var newSpeciality = new Specialty()
            {
                Code = specialty.Code,
                Name = specialty.Name
            };
            
            _specialitiesRepository.Add(newSpeciality);
            
            // save

            return newSpeciality.Id;
        }

        public async Task UpdateSpecialityAsync(SpecialtyDto specialty)
        {
            var specialityToUpdate = await _specialitiesRepository.GetByIdAsync(specialty.Id);

            if (specialityToUpdate == null)
            {
                return;
            }
            
            specialityToUpdate.Name = specialty.Name;
            specialityToUpdate.Code = specialty.Code;
            
            // save
        }

        public async Task DeleteSpecialityAsync(int specialityId)
        {
            var speciality = await _specialitiesRepository.GetByIdAsync(specialityId);

            if (speciality == null)
            {
                return;
            }
            
            _specialitiesRepository.Delete(speciality);
            
            // save

        }
    }
}