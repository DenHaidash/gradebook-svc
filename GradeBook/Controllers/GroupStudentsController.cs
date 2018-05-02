using System.Threading.Tasks;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("api/groups/{groupId:int}/students")]
    public class GroupStudentsController : Controller
    {
        private readonly IGroupStudentsService _groupStudentsService;

        public GroupStudentsController(IGroupStudentsService groupStudentsService)
        {
            _groupStudentsService = groupStudentsService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetGroupStudents(int groupId)
        {
            var students = await _groupStudentsService.GetStudentsAsync(groupId);

            return Ok(students);
        }
    }
}