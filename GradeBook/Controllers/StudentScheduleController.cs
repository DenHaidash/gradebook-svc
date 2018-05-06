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
    [Authorize(Roles = Roles.Student)]
    public class StudentScheduleController : Controller
    {
        private readonly IGroupScheduleService _groupScheduleService;

        public StudentScheduleController(IGroupScheduleService groupScheduleService)
        {
            _groupScheduleService = groupScheduleService;
        }
        
        private int AccountId => int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        
        /// <summary>
        /// Get current student's group subjects for current semester
        /// </summary>
        [HttpGet("api/students/self/group/semesters/current/courses")]
        [ProducesResponseType(typeof(IEnumerable<SemesterSubjectDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentStudentGroupSemesterSubjectsAsync()
        {
            var subjects = await _groupScheduleService.GetStudentGroupCurrentSemesterSubjects(AccountId);

            return Ok(subjects);
        }
    }
}