using System.Threading.Tasks;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("/api/groups/{groupId:int}/semesters/{year:int}/{semester:int}/courses/{courseId:int}/teachers")]
    public class TeacherSubjectsController : Controller
    {
        private readonly ITeacherCoursesService _teacherCoursesService;

        public TeacherSubjectsController(ITeacherCoursesService teacherCoursesService)
        {
            _teacherCoursesService = teacherCoursesService;
        }
        
        [HttpPut]
        public async Task<IActionResult> AssignTeacherToCourse(int groupId, int year, int semester, int courseId, [FromBody]int teacherId)
        {
            await _teacherCoursesService.AssignTeacherToCourseAsync(teacherId, year, groupId, courseId);

            return NoContent();
        }
        
        [HttpDelete]
        public async Task<IActionResult> UnassignTeacherToCourse(int groupId, int year, int semester, int courseId, [FromBody]int teacherId)
        {
            await _teacherCoursesService.UnassignTeacherFromCourseAsync(teacherId, year, groupId, courseId);

            return NoContent();
        }
    }
}