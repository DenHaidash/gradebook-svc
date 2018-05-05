using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.Common.Security;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Produces("application/json")]
    [Authorize(Roles = Roles.Admin)]
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
        
        /// <summary>
        /// Get teachers
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TeacherDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeachersAsync()
        {
            var teachers = await _teachersService.GetTeachersAsync();

            return Ok(teachers);
        }
        
        /// <summary>
        /// Get teacher
        /// </summary>
        [HttpGet("{teacherId:int}", Name = "GetTeacher")]
        [ProducesResponseType(typeof(TeacherDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTeacherAsync(int teacherId)
        {
            var teacher = await _teachersService.GetTeacherAsync(teacherId);

            if (teacher == null)
            {
                return NotFound();
            }
             
            return Ok(teacher);
        }
        
        /// <summary>
        /// Update teacher
        /// </summary>
        [HttpPost("{teacherId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditTeacherAsync(int teacherId, [FromBody]AccountViewModel account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacherDto = _mapper.Map<TeacherDto>(account);
            teacherDto.Id = teacherId;
            
            await _teachersService.UpdateTeacherAsync(teacherDto);

            return NoContent();
        }
        
        /// <summary>
        /// Delete teacher
        /// </summary>
        [HttpDelete("{teacherId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteTeacherAsync(int teacherId)
        {
            await _teachersService.DeleteTeacherAsync(teacherId);

            return NoContent();
        }
        
        /// <summary>
        /// Create teacher
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> CreateTeacherAsync([FromBody]NewAccountViewModel account)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var teacherId = await _teachersService.CreateTeacherAsync(_mapper.Map<TeacherDto>(account));

            return CreatedAtRoute("GetTeacher", new { teacherId }, null);
        }
    }
}