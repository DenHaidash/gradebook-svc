using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GradeBook.Common.Security;
using GradeBook.DTO;
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
        
        /// <summary>
        /// Get group semesters
        /// </summary>
        [HttpGet("{groupId:int:min(1)}/semesters")]
        [ProducesResponseType(typeof(IEnumerable<SemesterDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroupSemestersAsync(int groupId)
        {
            var semesters = await _groupSemestersService.GetGroupSemestersAsync(groupId);

            return Ok(semesters);
        }
        
        /// <summary>
        /// Get group's semester subjects
        /// </summary>
        [HttpGet("{groupId:int:min(1)}/semesters/{year:int:range(2000,2200)}/{semester:int:range(1,2)}/courses")]
        [ProducesResponseType(typeof(IEnumerable<SemesterSubjectDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroupSemesterSubjectsAsync(int groupId, int year, int semester)
        {
            var subjects = await _groupScheduleService.GetGroupSemesterSubjects(groupId, year, semester);

            return Ok(subjects);
        }
        
        /// <summary>
        /// Add subject to group's semester
        /// </summary>
        [HttpPut("{groupId:int:min(1)}/semesters/{year:int:range(2000,2200)}/{semester:int:range(1,2)}/courses")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddGroupSemesterSubjectAsync(int groupId, int year, int semester, [FromBody]SemesterSubjectViewModel subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }
            
            await _groupScheduleService.AddSubjectToSemester(_mapper.Map<SemesterSubjectDto>(subject), groupId, year, semester);

            return NoContent();
        }
        
        /// <summary>
        /// Delete subject from group's semester
        /// </summary>
        [HttpDelete("{groupId:int:min(1)}/semesters/{year:int:range(2000,2200)}/{semester:int:range(1,2)}/courses/{courseId:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveGroupSemesterSubjectAsync(int groupId, int year, int semester, int courseId)
        {
            await _groupScheduleService.DeleteSubjectFromSemester(courseId, groupId, year, semester);

            return NoContent();
        }
    }
}