using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Authorize]
    [Route("api/groups")]
    public class GroupsController : Controller
    {
        private readonly IGroupsService _groupsService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupsService groupsService, IMapper mapper)
        {
            _groupsService = groupsService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetGroupsAsync()
        {
            var groups = await _groupsService.GetGroupsAsync();

            return Ok(groups);
        }
        
        [HttpGet("{groupId:int}", Name = "GetGroup")]
        public async Task<IActionResult> GetGroupAsync(int groupId)
        {
            var group = await _groupsService.GetGroupAsync(groupId);

            if (group == null)
            {
                return NotFound();
            }
            
            return Ok(group);
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateGroupAsync([FromBody]GroupViewModel group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var groupId = await _groupsService.CreateGroupAsync(_mapper.Map<GroupDto>(group));

            return CreatedAtRoute("GetGroup", new { groupId }, null);
        }
        
        [HttpDelete("{groupId:int}")]
        public async Task<IActionResult> DeleteGroupAsync(int groupId)
        {
            await _groupsService.RemoveGroupAsync(groupId);
            
            return NoContent();
        }
        
        [HttpPost("{groupId:int}")]
        public async Task<IActionResult> UpdateGroupAsync(int groupId, [FromBody]GroupViewModel group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var groupDto = _mapper.Map<GroupDto>(group);
            groupDto.Id = groupId;
            
            await _groupsService.UpdateGroupAsync(groupDto);
            
            return NoContent();
        }
    }
}