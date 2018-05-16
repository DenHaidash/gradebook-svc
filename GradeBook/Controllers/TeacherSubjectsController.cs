using System.Threading.Tasks;
using GradeBook.Common.Security;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using GradeBook.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Produces("application/json")]
    [Authorize(Roles = Roles.Admin)]
    [Route("/api/groups/{groupId:int:min(1)}/semesters/{year:int:range(2000,2200)}/{semester:int:range(1,2)}/courses/{courseId:int:min(1)}/teachers")]
    public class TeacherSubjectsController : Controller
    {
        private readonly ITeacherCoursesService _teacherCoursesService;

        public TeacherSubjectsController(ITeacherCoursesService teacherCoursesService)
        {
            _teacherCoursesService = teacherCoursesService;
        }
        
        /// <summary>
        /// Assign teacher to group's subject
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AssignTeacherToCourseAsync(int groupId, int year, int semester, int courseId, [FromBody]TeacherSubjectAssignmentViewModel teacherAssignment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }
            
            await _teacherCoursesService.AssignTeacherToCourseAsync(teacherAssignment.TeacherId, year, semester, groupId, courseId);

            return NoContent();
        }
        
        /// <summary>
        /// Unassign teacher from group's subject
        /// </summary>
        [HttpDelete("{teacherId:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UnassignTeacherToCourseAsync(int groupId, int year, int semester, int courseId, int teacherId)
        {
            await _teacherCoursesService.UnassignTeacherFromCourseAsync(teacherId, year, semester, groupId, courseId);

            return NoContent();
        }
    }
}