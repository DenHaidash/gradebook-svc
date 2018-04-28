using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Authorize]
    [Route("api/teachers")]
    public class TeachersController : Controller
    {
        private readonly ITeachersService _teachersService;
        private readonly IMapper _mapper;

        public TeachersController(ITeachersService teachersService, IMapper mapper)
        {
            _teachersService = teachersService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _teachersService.GetTeachersAsync();

            return Ok(teachers);
        }
        
        [HttpGet("~/api/specialities/{specialityId:int}/teachers")]
        public async Task<IActionResult> GetTeachersWithSpeciality(int specialityId)
        {
            var teachers = await _teachersService.GetTeachersAsync();

            return Ok(teachers);
        }
        
        [HttpGet("{teacherId:int}", Name = "GetTeacher")]
        public async Task<IActionResult> GetTeacher(int teacherId)
        {
            var teacher = await _teachersService.GetTeacherAsync(teacherId);

            if (teacher == null)
            {
                return NotFound();
            }
             
            return Ok(teacher);
        }
        
        [HttpPost("{teacherId:int}")]
        public async Task<IActionResult> EditTeacher(int teacherId, [FromBody]TeacherViewModel teacher)
        {
            if (teacher == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var teacherDto = _mapper.Map<TeacherDto>(teacher);
            
            teacherDto.Id = teacherId;
            
            await _teachersService.UpdateTeacherAsync(teacherDto);

            return NoContent();
        }
        
        [HttpDelete("{teacherId:int}")]
        public async Task<IActionResult> DeleteTeacher(int teacherId)
        {
            await _teachersService.DeleteTeacherAsync(teacherId);

            return NoContent();
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateTeacher([FromBody]TeacherViewModel teacher)
        {
            if (teacher == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var teacherId = await _teachersService.CreateTeacherAsync(_mapper.Map<TeacherDto>(teacher));

            return CreatedAtRoute("GetTeacher", new { teacherId }, null);
        }
    }
}