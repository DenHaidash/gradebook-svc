using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;

namespace GradeBook.Services.Abstactions
{
    public interface IAssestmentTypesService
    {
        Task<IEnumerable<AssestmentTypeDto>> GetAssestmentTypesAsync();
    }
}