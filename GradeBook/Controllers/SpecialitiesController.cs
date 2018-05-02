using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Authorize]
    [Route("api/specialities")]
    public class SpecialitiesController : Controller
    {
        private readonly ISpecialitiesService _specialitiesService;
        private readonly IMapper _mapper;

        public SpecialitiesController(ISpecialitiesService specialitiesService, IMapper mapper)
        {
            _specialitiesService = specialitiesService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSpecialitiesAsync()
        {
            var specialities = await _specialitiesService.GetSpecialitiesAsync();

            return Ok(specialities);
        }
        
        [HttpGet("{specialtyId:int}", Name = "GetSpeciality")]
        public async Task<IActionResult> GetSpecialityAsync(int specialtyId)
        {
            var specialty = await _specialitiesService.GetSpecialityAsync(specialtyId);

            if (specialty == null)
            {
                return NotFound();
            }
            
            return Ok(specialty);
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateSpecialityAsync([FromBody]SpecialtyViewModel specialty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var specialtyId = await _specialitiesService.CreateSpecialityAsync(_mapper.Map<SpecialtyDto>(specialty));

            return CreatedAtRoute("GetSpeciality", new { specialtyId }, null);
        }
        
        [HttpPost("{specialtyId:int}")]
        public async Task<IActionResult> UpdateSpecialityAsync(int specialtyId, [FromBody]SpecialtyViewModel specialty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var specialtyDto = _mapper.Map<SpecialtyDto>(specialty);
            specialtyDto.Id = specialtyId;
            
            await _specialitiesService.UpdateSpecialityAsync(specialtyDto);

            return NoContent();
        }
        
        [HttpDelete("{specialtyId:int}")]
        public async Task<IActionResult> DeleteSpecialityAsync(int specialtyId)
        {
            await _specialitiesService.DeleteSpecialityAsync(specialtyId);

            return NoContent();
        }
    }
}