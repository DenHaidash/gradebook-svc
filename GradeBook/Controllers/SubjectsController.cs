using System.Threading.Tasks;
using GradeBook.DTO;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Route("/api/subjects")]
    public class SubjectsController : Controller
    {
        private readonly ISubjectsService _subjectsService;

        public SubjectsController(ISubjectsService subjectsService)
        {
            _subjectsService = subjectsService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSubjectsAsync()
        {
            var subjects = await _subjectsService.GetSubjectsAsync();

            return Ok(subjects);
        }
        
        [HttpGet("{subjectId:int}")]
        public async Task<IActionResult> GetSubjectAsync(int subjectId)
        {
            var subject = await _subjectsService.GetSubjectAsync(subjectId);

            return Ok(subject);
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateSubject([FromBody]SubjectDto subject)
        {
            if (subject == null)
            {
                return BadRequest();
            }
            
            var subjectId = await _subjectsService.CreateSubjectAsync(subject);

            return CreatedAtAction("GetSubjectAsync", subjectId);
        }
        
        [HttpPost("{subjectId:int}")]
        public async Task<IActionResult> UpdateSubject(int subjectId, [FromBody]SubjectDto subject) // todo use ViewModel
        {
            if (subject == null)
            {
                return BadRequest();
            }

            subject.Id = subjectId;
            
            await _subjectsService.UpdateSubjectAsync(subject);

            return NoContent();
        }
        
        [HttpDelete("{subjectId:int}")]
        public async Task<IActionResult> DeleteSubject(int subjectId)
        {
            await _subjectsService.DeleteSubjectAsync(subjectId);

            return NoContent();
        }
    }
}