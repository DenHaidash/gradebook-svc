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
    public class GroupsController : Controller
    {
        private readonly IGroupsService _groupsService;
        private readonly IMapper _mapper;

        public GroupsController(IGroupsService groupsService, IMapper mapper)
        {
            _groupsService = groupsService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Get groups
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GroupDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGroupsAsync()
        {
            var groups = await _groupsService.GetGroupsAsync();

            return Ok(groups);
        }
        
        /// <summary>
        /// Get group
        /// </summary>
        [HttpGet("{groupId:int:min(1)}", Name = "GetGroup")]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetGroupAsync(int groupId)
        {
            var group = await _groupsService.GetGroupAsync(groupId);

            if (group == null)
            {
                return NotFound();
            }
            
            return Ok(group);
        }
        
        /// <summary>
        /// Create group
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(GroupDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateGroupAsync([FromBody]GroupViewModel group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }
            
            var newGroup = await _groupsService.CreateGroupAsync(_mapper.Map<GroupDto>(group), group.EducationStartedAt);

            return CreatedAtRoute("GetGroup", new { groupId = newGroup.Id }, newGroup);
        }
        
        /// <summary>
        /// Delete group
        /// </summary>
        [HttpDelete("{groupId:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteGroupAsync(int groupId)
        {
            await _groupsService.RemoveGroupAsync(groupId);
            
            return NoContent();
        }
        
        /// <summary>
        /// Update group
        /// </summary>
        [HttpPost("{groupId:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateGroupAsync(int groupId, [FromBody]GroupViewModel group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }

            var groupDto = _mapper.Map<GroupDto>(group);
            groupDto.Id = groupId;
            
            await _groupsService.UpdateGroupAsync(groupDto);
            
            return NoContent();
        }
    }
}