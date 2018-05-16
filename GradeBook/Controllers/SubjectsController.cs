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
    [Route("api/subjects")]
    public class SubjectsController : Controller
    {
        private readonly ISubjectsService _subjectsService;
        private readonly IMapper _mapper;

        public SubjectsController(ISubjectsService subjectsService, IMapper mapper)
        {
            _subjectsService = subjectsService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Get subjects
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SubjectDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSubjectsAsync()
        {
            var subjects = await _subjectsService.GetSubjectsAsync();

            return Ok(subjects);
        }
        
        /// <summary>
        /// Get subject
        /// </summary>
        [HttpGet("{subjectId:int:min(1)}", Name = "GetSubject")]
        [ProducesResponseType(typeof(SubjectDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubjectAsync(int subjectId)
        {
            var subject = await _subjectsService.GetSubjectAsync(subjectId);

            if (subject == null)
            {
                return NotFound();
            }
            
            return Ok(subject);
        }
        
        /// <summary>
        /// Create subject
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(SubjectDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSubjectAsync([FromBody]SubjectViewModel subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }
            
            var newSubject = await _subjectsService.CreateSubjectAsync(_mapper.Map<SubjectDto>(subject));

            return CreatedAtRoute("GetSubject", new { subjectId = newSubject.Id }, newSubject);
        }
        
        /// <summary>
        /// Update subject
        /// </summary>
        [HttpPost("{subjectId:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSubjectAsync(int subjectId, [FromBody]SubjectViewModel subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }
            
            var subjectDto = _mapper.Map<SubjectDto>(subject);
            subjectDto.Id = subjectId;
            
            await _subjectsService.UpdateSubjectAsync(subjectDto);

            return NoContent();
        }
        
        /// <summary>
        /// Delete subject
        /// </summary>
        [HttpDelete("{subjectId:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSubjectAsync(int subjectId)
        {
            await _subjectsService.DeleteSubjectAsync(subjectId);

            return NoContent();
        }
    }
}