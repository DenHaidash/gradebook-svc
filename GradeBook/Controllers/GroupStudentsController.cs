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
    [Route("api/groups/{groupId:int}/students")]
    public class GroupStudentsController : Controller
    {
        private readonly IGroupStudentsService _groupStudentsService;

        public GroupStudentsController(IGroupStudentsService groupStudentsService)
        {
            _groupStudentsService = groupStudentsService;
        }
        
        /// <summary>
        /// Get group's students
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StudentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroupStudentsAsync(int groupId)
        {
            var students = await _groupStudentsService.GetStudentsAsync(groupId);

            return Ok(students);
        }
    }
}