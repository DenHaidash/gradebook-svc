using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("api/groups")]
    public class GroupScheduleController : Controller
    {
        private readonly IGroupScheduleService _groupScheduleService;
        private readonly IGroupSemestersService _groupSemestersService;
        private readonly IMapper _mapper;

        public GroupScheduleController(IGroupScheduleService groupScheduleService, IGroupSemestersService groupSemestersService, IMapper mapper)
        {
            _groupScheduleService = groupScheduleService;
            _groupSemestersService = groupSemestersService;
            _mapper = mapper;
        }
        
        [HttpGet("{groupId:int}/semesters")]
        public async Task<IActionResult> GetGroupSemesters(int groupId)
        {
            var semesters = await _groupSemestersService.GetGroupSemestersAsync(groupId);

            return Ok(semesters);
        }
        
        [HttpGet("{groupId:int}/semesters/{year:int}/{semester:int}/courses")]
        public async Task<IActionResult> GetGroupSemesterSubjects(int groupId, int year, int semester)
        {
            var subjects = await _groupScheduleService.GetGroupSemestedSubjects(groupId, year, semester);

            return Ok(subjects);
        }
        
        [HttpPut("{groupId:int}/semesters/{year:int}/{semester:int}/courses")]
        public async Task<IActionResult> AddGroupSemesterSubject(int groupId, int year, int semester, [FromBody]SemesterSubjectViewModel subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var semesterSubjectDto = _mapper.Map<SemesterSubjectDto>(subject);
            
            await _groupScheduleService.AddSubjectToSemester(semesterSubjectDto, groupId, year, semester);

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