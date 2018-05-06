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
    [Route("api/teachers")]
    public class TeacherGroupController : Controller
    {
        private readonly ITeacherCoursesService _teacherCoursesService;

        public TeacherGroupController(ITeacherCoursesService teacherCoursesService)
        {
            _teacherCoursesService = teacherCoursesService;
        }
        
        private int AccountId => int.Parse(User.Claims.First(c => c.Type == ClaimTypes.Sid).Value);
        
        /// <summary>
        /// Get teacher's groups for the semester
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("{teacherId:int:min(1)}/semesters/{year:int:range(2000,2200)}/{semester:int:range(1,2)}/groups")]
        [ProducesResponseType(typeof(IEnumerable<GroupDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeacherSemesterGroupsAsync(int teacherId, int year, int semester)
        {
            var groups = await _teacherCoursesService.GetTeacherSemesterGroupsAsync(teacherId, year, semester);

            return Ok(groups);
        }
         
        /// <summary>
        /// Get current teacher's groups for the current semester
        /// </summary>
        [Authorize(Roles = Roles.Teacher)]
        [HttpGet("self/semesters/current/groups")]
        [ProducesResponseType(typeof(IEnumerable<GroupDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentTeacherCurrentSemesterGroupsAsync()
        {
            var groups = await _teacherCoursesService.GetTeacherCurrentSemesterGroupsAsync(AccountId);

            return Ok(groups);
        }
        
        /// <summary>
        /// Get teacher's group subjects for the semester
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("{teacherId:int:min(1)}/semesters/{year:int:range(2000,2200)}/{semester:int:range(1,2)}/groups/{groupId:int:min(1)}/courses")]
        [ProducesResponseType(typeof(IEnumerable<SubjectDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeacherSemesterGroupsCoursesAsync(int teacherId, int year, int semester, int groupId)
        {
            var courses = await _teacherCoursesService.GetTeacherSemesterGroupCoursesAsync(teacherId, year, semester, groupId);

            return Ok(courses);
        }
        
        /// <summary>
        /// Get current teacher's group subjects for the current semester
        /// </summary>
        [Authorize(Roles = Roles.Teacher)]
        [HttpGet("self/semesters/current/groups/{groupId:int:min(1)}/courses")]
        [ProducesResponseType(typeof(IEnumerable<SubjectDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCurrentTeacherCurrentSemesterGroupsCoursesAsync(int groupId)
        {
            var courses = await _teacherCoursesService.GetTeacherCurrentSemesterGroupCoursesAsync(AccountId, groupId);

            return Ok(courses);
        }
    }
}