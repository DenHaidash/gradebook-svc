using System.Threading.Tasks;
using AutoMapper;
using GradeBook.DTO;
using GradeBook.Models;
using GradeBook.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradeBook.Controllers
{
    [Authorize]
    [Route("/api/subjects")]
    public class SubjectsController : Controller
    {
        private readonly ISubjectsService _subjectsService;
        private readonly IMapper _mapper;

        public SubjectsController(ISubjectsService subjectsService, IMapper mapper)
        {
            _subjectsService = subjectsService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetSubjectsAsync()
        {
            var subjects = await _subjectsService.GetSubjectsAsync();

            return Ok(subjects);
        }
        
        [HttpGet("{subjectId:int}", Name = "GetSubject")]
        public async Task<IActionResult> GetSubjectAsync(int subjectId)
        {
            var subject = await _subjectsService.GetSubjectAsync(subjectId);

            if (subject == null)
            {
                return NotFound();
            }
            
            return Ok(subject);
        }
        
        [HttpPut]
        public async Task<IActionResult> CreateSubjectAsync([FromBody]SubjectViewModel subject)
        {
            if (subject == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var subjectId = await _subjectsService.CreateSubjectAsync(_mapper.Map<SubjectDto>(subject));

            return CreatedAtRoute("GetSubject", new {  subjectId }, null);
        }
        
        [HttpPost("{subjectId:int}")]
        public async Task<IActionResult> UpdateSubjectAsync(int subjectId, [FromBody]SubjectViewModel subject)
        {
            if (subject == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var subjectDto = _mapper.Map<SubjectDto>(subject);
            subjectDto.Id = subjectId;
            
            await _subjectsService.UpdateSubjectAsync(subjectDto);

            return NoContent();
        }
        
        [HttpDelete("{subjectId:int}")]
        public async Task<IActionResult> DeleteSubjectAsync(int subjectId)
        {
            await _subjectsService.DeleteSubjectAsync(subjectId);

            return NoContent();
        }
    }
}