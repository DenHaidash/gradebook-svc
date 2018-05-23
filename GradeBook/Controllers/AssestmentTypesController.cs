using System.Collections.Generic;
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
    [Authorize(Roles = Roles.Admin)]
    [Route("api/assessment-types")]
    public class AssestmentTypesController : Controller
    {
        private readonly IAssestmentTypesService _assestmentTypesService;

        public AssestmentTypesController(IAssestmentTypesService assestmentTypesService)
        {
            _assestmentTypesService = assestmentTypesService;
        }
        
        /// <summary>
        /// Get assestment types
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AssestmentTypeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssestmentTypesAsync()
        {
            var assestmentTypes = await _assestmentTypesService.GetAssestmentTypesAsync();

            return Ok(assestmentTypes);
        }
    }
}