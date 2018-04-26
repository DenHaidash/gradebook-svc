using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("api/specialities")]
    public class SpecialitiesController : Controller
    {
        private readonly ISpecialitiesService _specialitiesService;

        public SpecialitiesController(ISpecialitiesService specialitiesService)
        {
            _specialitiesService = specialitiesService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSpecialities()
        {
            var specialities = await _specialitiesService.GetSpecialitiesAsync();

            return Ok(specialities);
        }
        
        [HttpGet("{specialtyId:int}")]
        public async Task<IActionResult> GetSpeciality(int specialtyId)
        {
            var specialty = await _specialitiesService.GetSpecialityAsync(specialtyId);

            return Ok(specialty);
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateSpeciality([FromBody]SpecialtyDto specialty)
        {
            if (specialty == null)
            {
                return BadRequest();
            }
            
            var specialtyId = await _specialitiesService.CreateSpecialityAsync(specialty);

            return CreatedAtAction("GetSpeciality", specialtyId);
        }
        
        [HttpPost("{specialtyId:int}")]
        public async Task<IActionResult> UpdateSpeciality(int specialtyId, [FromBody]SpecialtyDto specialty)
        {
            if (specialty == null)
            {
                return BadRequest();
            }

            specialty.Id = specialtyId;
            
            await _specialitiesService.UpdateSpecialityAsync(specialty);

            return NoContent();
        }
    }
}