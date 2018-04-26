using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Interfaces
{
    public interface ISpecialitiesService
    {
        Task<IEnumerable<SpecialtyDto>> GetSpecialitiesAsync();
        Task<SpecialtyDto> GetSpecialityAsync(int specialtyId);
        Task<int> CreateSpecialityAsync(SpecialtyDto specialty);
        Task UpdateSpecialityAsync(SpecialtyDto specialty);
        Task DeleteSpecialityAsync(int specialityId);
    }
}