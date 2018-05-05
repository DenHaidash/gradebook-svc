using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Produces("application/json")]
    [Authorize]
    [Route("api/students/{studentId:int}/courses/{courseId:int}/grades")]
    public class GradesController : Controller
    {
        private readonly IStudentGradesService _studentGradesService;
        private readonly IMapper _mapper;

        public GradesController(IStudentGradesService studentGradesService, IMapper mapper)
        {
            _studentGradesService = studentGradesService;
            _mapper = mapper;
        }
        
        private int TeacherId => int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        
        /// <summary>
        /// Get student's grades for the subject
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(StudentSubjectGradesDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentGradesAsync(int studentId, int courseId)
        {
            var studentSubjectGrades = await _studentGradesService.GetStudentSubjectCurrentGradesAsync(studentId, courseId);

            return Ok(studentSubjectGrades);
        }
        
        /// <summary>
        /// Create student's grade for the subject
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStudentGradeAsync(int studentId, int courseId, [FromBody]GradeViewModel grade)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            await _studentGradesService.AddStudentCourseGradeAsync(_mapper.Map<GradeDto>(grade), studentId, TeacherId, courseId);

            return NoContent();
        }
        
        /// <summary>
        /// Delete student's grade
        /// </summary>
        [HttpDelete("{gradeId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteStudentGradeAsync(int gradeId)
        {            
            await _studentGradesService.RemoveStudentCourseGradeAsync(gradeId, TeacherId);

            return NoContent();
        }
        
        /// <summary>
        /// Get student's final grade for the subject
        /// </summary>
        [HttpGet("final")]
        [ProducesResponseType(typeof(GradeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentFinalGradeAsync(int studentId, int courseId)
        {
            var grade = await _studentGradesService.GetStudentSubjectFinalGradeAsync(studentId, courseId);

            if (grade == null)
            {
                return NotFound();
            }
            
            return Ok(grade);
        }
        
        
        /// <summary>
        /// Create student's final grade for the subject
        /// </summary>
        [HttpPut("final")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ConfirmStudentFinalGradeAsync(int studentId, int courseId)
        {
            await _studentGradesService.ConfirmStudentCourseFinalGradeAsync(studentId, TeacherId, courseId);
            
            return NoContent();
        }
    }
}