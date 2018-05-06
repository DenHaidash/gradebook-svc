using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GradeBook.Common.Security;
using GradeBook.DTO;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Produces("application/json")]
    public class StudentGradesController : Controller
    {
        private readonly IStudentGradesService _studentGradesService;

        public StudentGradesController(IStudentGradesService studentGradesService)
        {
            _studentGradesService = studentGradesService;
        }
        
        private int AccountId => int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        
        /// <summary>
        /// Get student's final grades
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("api/students/{studentId:int:min(1)}/grades")]
        [ProducesResponseType(typeof(IEnumerable<GradeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentFinalGradesAsync(int studentId)
        {
            var studentFinalGrades = await _studentGradesService.GetStudentFinalGradesAsync(studentId);

            return Ok(studentFinalGrades);
        }
        
        /// <summary>
        /// Get current student's final grades
        /// </summary>
        [Authorize(Roles = Roles.Student)]
        [HttpGet("api/students/self/grades")]
        [ProducesResponseType(typeof(IEnumerable<GradeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentStudentFinalGradesAsync()
        {
            var studentFinalGrades = await _studentGradesService.GetStudentFinalGradesAsync(AccountId);

            return Ok(studentFinalGrades);
        }
        
        /// <summary>
        /// Get current student's grades for the subject
        /// </summary>
        [Authorize(Roles = Roles.Student)]
        [HttpGet("api/students/self/courses/{courseId:int:min(1)}/grades")]
        [ProducesResponseType(typeof(StudentSubjectGradesDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentStudentGradesAsync(int courseId)
        {
            var studentSubjectGrades = await _studentGradesService.GetStudentSubjectCurrentGradesAsync(AccountId, courseId);

            return Ok(studentSubjectGrades);
        }
    }
}
