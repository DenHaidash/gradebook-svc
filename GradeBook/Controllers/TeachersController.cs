using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("api/teachers")]
    public class TeachersController : Controller
    {
        private readonly ITeachersService _teachersService;

        public TeachersController(ITeachersService teachersService)
        {
            _teachersService = teachersService;
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
        
        [HttpGet("{teacherId:int}")]
        public async Task<IActionResult> GetTeacher(int teacherId)
        {
            var teachers = await _teachersService.GetTeacherAsync(teacherId);

            return Ok(teachers);
        }
        
        [HttpPost("{teacherId:int}")]
        public async Task<IActionResult> EditTeacher(int teacherId, [FromBody]TeacherDto teacher)
        {
            if (teacher == null)
            {
                return BadRequest();
            }

            teacher.Id = teacherId;
            
            await _teachersService.UpdateTeacherAsync(teacher);

            return NoContent();
        }
        
        [HttpDelete("{teacherId:int}")]
        public async Task<IActionResult> DeleteTeacher(int teacherId)
        {
            await _teachersService.DeleteTeacherAsync(teacherId);

            return NoContent();
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateTeacher(TeacherDto teacher)
        {
            var teacherId = await _teachersService.CreateTeacherAsync(teacher);

            return CreatedAtAction("GetTeacher", teacherId);
        }
    }
}