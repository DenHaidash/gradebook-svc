using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("api/students/{studentId:int}/courses/{courseId:int}/grades")]
    public class GradesController : Controller
    {
        private readonly IStudentGradesService _studentGradesService;

        public GradesController(IStudentGradesService studentGradesService)
        {
            _studentGradesService = studentGradesService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetStudentGrades(int studentId, int courseId)
        {
            var grades = await _studentGradesService.GetStudentSubjectCurrentGradesAsync(studentId, courseId);

            return Ok(grades);
        }
        
        [HttpPut]
        public async Task<IActionResult> AddStudentGrade(int studentId, int courseId, [FromBody]GradeDto grade)
        {
            if (grade == null)
            {
                return BadRequest();
            }

            grade.Teacher.Id = 0;
            grade.Student.Id = studentId;
            grade.Gradebook.Subject.Id = courseId;
            
            await _studentGradesService.AddStudentCourseGradeAsync(grade, studentId);

            return NoContent();
        }
        
        [HttpDelete("{gradeId:int}")]
        public async Task<IActionResult> AddStudentGrade(int studentId, int gradeId)
        {            
            await _studentGradesService.RemoveStudentCourseGradeAsync(gradeId, studentId);

            return NoContent();
        }
        
        [HttpGet("final")]
        public async Task<IActionResult> GetStudentFinalGrade(int studentId, int courseId)
        {
            var grade = await _studentGradesService.GetStudentSubjectFinalGradeAsync(studentId, courseId);

            if (grade == null)
            {
                return NotFound();
            }
            
            return Ok(grade);
        }
        
        [HttpPut("final")]
        public async Task<IActionResult> ConfirmStudentFinalGrade(int studentId, int courseId)
        {
            await _studentGradesService.ConfirmStudentCourseFinalGradeAsync(studentId, courseId);

            
            return NoContent();
        }
    }
}