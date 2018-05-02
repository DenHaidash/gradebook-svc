using System;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
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
        
        [HttpGet("{studentId:int}", Name = "GetStudent")]
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
        public async Task<IActionResult> UpdateStudent(int studentId, [FromBody]AccountViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var studentDto = _mapper.Map<StudentDto>(student);
            studentDto.Id = studentId;
            
            await _studentsService.UpdateStudentAsync(studentDto);
            
            return NoContent();
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateStudent([FromBody]NewStudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var studentId = await _studentsService.CreateStudentAsync(_mapper.Map<StudentDto>(student));
            
            return CreatedAtRoute("GetStudent", new { studentId }, null);
        }
    }
}