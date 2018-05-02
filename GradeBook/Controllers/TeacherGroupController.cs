using System.Threading.Tasks;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("api/teachers")]
    public class TeacherGroupController : Controller
    {
        private readonly ITeacherCoursesService _teacherCoursesService;

        public TeacherGroupController(ITeacherCoursesService teacherCoursesService)
        {
            _teacherCoursesService = teacherCoursesService;
        }
        
        [HttpGet("{teacherId:int}/semesters/{year:int}/{semester:int}/groups")]
        public async Task<IActionResult> GetTeacherSemesterGroups(int teacherId, int year, int semester)
        {
            var groups = await _teacherCoursesService.GetTeacherSemesterGroupsAsync(teacherId, year, semester);

            return Ok(groups);
        }
                
        [HttpGet("{teacherId:int}/semesters/{year:int}/{semester:int}/groups/{groupId:int}/courses")]
        public async Task<IActionResult> GetTeacherSemesterGroupsCourses(int teacherId, int year, int semester, int groupId)
        {
            var courses = await _teacherCoursesService.GetTeacherSemesterGroupCoursesAsync(teacherId, year, semester, groupId);

            return Ok(courses);
        }
    }
}