using System.Threading.Tasks;
using GradeBook.Services.Abstactions;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("api/assestment-types")]
    public class AssestmentTypesController : Controller
    {
        private readonly IAssestmentTypesService _assestmentTypesService;

        public AssestmentTypesController(IAssestmentTypesService assestmentTypesService)
        {
            _assestmentTypesService = assestmentTypesService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAssestmentTypesAsync()
        {
            var assestmentTypes = await _assestmentTypesService.GetAssestmentTypesAsync();

            return Ok(assestmentTypes);
        }
    }
}