﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GradeBook.Common.Security;
using GradeBook.DTO;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Produces("application/json")]
    [Authorize(Roles = Roles.Teacher+","+Roles.Admin)]
    [Route("api/groups/{groupId:int:min(1)}/students")]
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