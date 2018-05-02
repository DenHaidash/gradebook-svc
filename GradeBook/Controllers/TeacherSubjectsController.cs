using System.Threading.Tasks;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
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
        public async Task<IActionResult> AssignTeacherToCourseAsync(int groupId, int year, int semester, int courseId, [FromBody]TeacherSubjectAssignmentViewModel teacherAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            await _teacherCoursesService.AssignTeacherToCourseAsync(teacherAssignment.TeacherId, year, semester, groupId, courseId);

            return NoContent();
        }
        
        [HttpDelete("{teacherId:int}")]
        public async Task<IActionResult> UnassignTeacherToCourseAsync(int groupId, int year, int semester, int courseId, int teacherId)
        {
            await _teacherCoursesService.UnassignTeacherFromCourseAsync(teacherId, year, semester, groupId, courseId);

            return NoContent();
        }
    }
}