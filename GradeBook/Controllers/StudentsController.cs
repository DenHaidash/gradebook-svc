using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("api/students")]
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }
        
        [HttpGet("{studentId:int}")]
        public async Task<IActionResult> GetStudent(int studentId)
        {
            var student = await _studentsService.GetStudentAsync(studentId);

            if (student == null)
            {
                return NotFound();
            }
            
            return Ok(student);
        }
        
        [HttpDelete("{studentId:int}")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            await _studentsService.DeleteStudentAsync(studentId);
            
            return NoContent();
        }
        
        [HttpPost("{studentId:int}")]
        public async Task<IActionResult> UpdateStudent(int studentId, [FromBody]StudentDto student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            student.Id = studentId;
            
            await _studentsService.UpdateStudentAsync(student);
            
            return NoContent();
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateStudent([FromBody]StudentDto student)
        {
            if (student == null)
            {
                return BadRequest();
            }
            
            var studentId = await _studentsService.CreateStudentAsync(student);
            
            return CreatedAtAction("GetStudent", studentId);
        }
    }
}