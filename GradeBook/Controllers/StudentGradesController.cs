using System.Threading.Tasks;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    public class StudentGradesController : Controller
    {
        private readonly IStudentGradesService _studentGradesService;

        public StudentGradesController(IStudentGradesService studentGradesService)
        {
            _studentGradesService = studentGradesService;
        }
        
        [HttpGet("api/students/{studentId:int}/grades")]
        public async Task<IActionResult> GetStudentFinalGrades(int studentId)
        {
            var studentFinalGrades = await _studentGradesService.GetStudentFinalGradesAsync(studentId);

            return Ok(studentFinalGrades);
        }
    }
}