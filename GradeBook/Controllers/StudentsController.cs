using System.Threading.Tasks;
using AutoMapper;
using GradeBook.Common.Security;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using GradeBook.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Produces("application/json")]
    [Route("api/students")]
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;
        private readonly IMapper _mapper;

        public StudentsController(IStudentsService studentsService, IMapper mapper)
        {
            _studentsService = studentsService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Get student
        /// </summary>
        [Authorize(Roles = Roles.Admin+","+Roles.Teacher)]
        [HttpGet("{studentId:int:min(1)}", Name = "GetStudent")]
        [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentAsync(int studentId)
        {
            var student = await _studentsService.GetStudentAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }
            
            return Ok(student);
        }
        
        /// <summary>
        /// Delete student
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{studentId:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteStudentAsync(int studentId)
        {
            await _studentsService.DeleteStudentAsync(studentId);
            
            return NoContent();
        }
        
        /// <summary>
        /// Update student
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost("{studentId:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStudentAsync(int studentId, [FromBody]AccountViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }

            var studentDto = _mapper.Map<StudentDto>(student);
            studentDto.Id = studentId;
            
            await _studentsService.UpdateStudentAsync(studentDto);
            
            return NoContent();
        }
        
        /// <summary>
        /// Create student
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpPut]
        [ProducesResponseType(typeof(StudentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateStudentAsync([FromBody]NewStudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }
            
            var newStudent = await _studentsService.CreateStudentAsync(_mapper.Map<StudentDto>(student));
            
            return CreatedAtRoute("GetStudent", new { studentId = newStudent.Id }, newStudent);
        }
    }
}