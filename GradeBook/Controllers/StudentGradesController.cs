using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class StudentGradesController : Controller
    {
        private readonly IStudentGradesService _studentGradesService;

        public StudentGradesController(IStudentGradesService studentGradesService)
        {
            _studentGradesService = studentGradesService;
        }
        
        /// <summary>
        /// Get student's final grades
        /// </summary>
        [HttpGet("api/students/{studentId:int}/grades")]
        [ProducesResponseType(typeof(IEnumerable<GradeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentFinalGradesAsync(int studentId)
        {
            var studentFinalGrades = await _studentGradesService.GetStudentFinalGradesAsync(studentId);

            return Ok(studentFinalGrades);
        }
    }
}
