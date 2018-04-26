using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        private readonly IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetGroups()
        {
            var groups = await _groupsService.GetGroupsAsync();

            return Ok(groups);
        }
        
        [HttpGet("{groupId:int}")]
        public async Task<IActionResult> GetGroup(int groupId)
        {
            var group = await _groupsService.GetGroupAsync(groupId);

            if (group == null)
            {
                return NotFound();
            }
            
            return Ok(group);
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateGroup([FromBody]GroupDto group)
        {
            if (group == null)
            {
                return BadRequest();
            }
           
            var groupId = await _groupsService.CreateGroupAsync(group);

            return CreatedAtAction("GetGroup", groupId);
        }
        
        [HttpDelete("{groupId:int}")]
        public async Task<IActionResult> DeleteGroup(int groupId)
        {
            await _groupsService.RemoveGroupAsync(groupId);
            
            return NoContent();
        }
        
        [HttpPost("{groupId:int}")]
        public async Task<IActionResult> UpdateGroup(int groupId, [FromBody]GroupDto group)
        {
            if (group == null)
            {
                return BadRequest();
            }

            group.Id = groupId;
            
            await _groupsService.UpdateGroupAsync(group);
            
            return NoContent();
        }
    }
}