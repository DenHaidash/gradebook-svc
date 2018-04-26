using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("api/groups")]
    public class GroupScheduleController : Controller
    {
        private readonly IGroupScheduleService _groupScheduleService;

        public GroupScheduleController(IGroupScheduleService groupScheduleService)
        {
            _groupScheduleService = groupScheduleService;
        }
        
        [HttpGet("{groupId:int}/semesters")]
        public async Task<IActionResult> GetGroupSemesters(int groupId)
        {
            var semesters = await _groupScheduleService.GetGroupSemestersAsync(groupId);

            return Ok(semesters);
        }
        
        [HttpGet("{groupId:int}/semesters/{year:int}/{semester:int}/courses")]
        public async Task<IActionResult> GetGroupSemesterSubjects(int groupId, int year, int semester)
        {
            var subjects = await _groupScheduleService.GetGroupSemestedSubjects(groupId, year, semester);

            return Ok(subjects);
        }
        
        [HttpPut("{groupId:int}/semesters/{year:int}/{semester:int}/courses")]
        public async Task<IActionResult> AddGroupSemesterSubject(int groupId, int year, int semester, SubjectDto subject)
        {
            if (subject == null)
            {
                return BadRequest();
            }
            
            await _groupScheduleService.AddSubjectToSemester(subject.Id, groupId, year, semester);

            return NoContent();
        }
        
        [HttpDelete("{groupId:int}/semesters/{year:int}/{semester:int}/courses/{courseId:int}")]
        public async Task<IActionResult> RemoveGroupSemesterSubject(int groupId, int year, int semester, int courseId)
        {
            await _groupScheduleService.DeleteSubjectFromSemester(courseId, groupId, year, semester);

            return NoContent();
        }
    }
}