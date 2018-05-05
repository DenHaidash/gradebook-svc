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
    [Route("api/teachers")]
    public class TeacherGroupController : Controller
    {
        private readonly ITeacherCoursesService _teacherCoursesService;

        public TeacherGroupController(ITeacherCoursesService teacherCoursesService)
        {
            _teacherCoursesService = teacherCoursesService;
        }
        
        /// <summary>
        /// Get teacher's groups for the semester
        /// </summary>
        [HttpGet("{teacherId:int}/semesters/{year:int}/{semester:int}/groups")]
        [ProducesResponseType(typeof(IEnumerable<GroupDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeacherSemesterGroupsAsync(int teacherId, int year, int semester)
        {
            var groups = await _teacherCoursesService.GetTeacherSemesterGroupsAsync(teacherId, year, semester);

            return Ok(groups);
        }
         
        /// <summary>
        /// Get teacher's group subjects for the semester
        /// </summary>
        [HttpGet("{teacherId:int}/semesters/{year:int}/{semester:int}/groups/{groupId:int}/courses")]
        [ProducesResponseType(typeof(IEnumerable<SubjectDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeacherSemesterGroupsCoursesAsync(int teacherId, int year, int semester, int groupId)
        {
            var courses = await _teacherCoursesService.GetTeacherSemesterGroupCoursesAsync(teacherId, year, semester, groupId);

            return Ok(courses);
        }
    }
}