﻿using System.Collections.Generic;
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
    [Route("api/specialities")]
    public class SpecialitiesController : Controller
    {
        private readonly ISpecialitiesService _specialitiesService;
        private readonly IMapper _mapper;

        public SpecialitiesController(ISpecialitiesService specialitiesService, IMapper mapper)
        {
            _specialitiesService = specialitiesService;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Get specialities
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SpecialtyDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSpecialitiesAsync()
        {
            var specialities = await _specialitiesService.GetSpecialitiesAsync();

            return Ok(specialities);
        }
        
        /// <summary>
        /// Get speciality
        /// </summary>
        [HttpGet("{specialtyId:int:min(1)}", Name = "GetSpeciality")]
        [ProducesResponseType(typeof(SpecialtyDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSpecialityAsync(int specialtyId)
        {
            var specialty = await _specialitiesService.GetSpecialityAsync(specialtyId);

            if (specialty == null)
            {
                return NotFound();
            }
            
            return Ok(specialty);
        }
        
        /// <summary>
        /// Create speciality
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(SpecialtyDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSpecialityAsync([FromBody]SpecialtyViewModel specialty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }
            
            var newSpecialty = await _specialitiesService.CreateSpecialityAsync(_mapper.Map<SpecialtyDto>(specialty));

            return CreatedAtRoute("GetSpeciality", new { specialtyId = newSpecialty.Id }, newSpecialty);
        }
        
        /// <summary>
        /// Update speciality
        /// </summary>
        [HttpPost("{specialtyId:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationError), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSpecialityAsync(int specialtyId, [FromBody]SpecialtyViewModel specialty)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ValidationError(ModelState));
            }

            var specialtyDto = _mapper.Map<SpecialtyDto>(specialty);
            specialtyDto.Id = specialtyId;
            
            await _specialitiesService.UpdateSpecialityAsync(specialtyDto);

            return NoContent();
        }
        
        /// <summary>
        /// Delete speciality
        /// </summary>
        [HttpDelete("{specialtyId:int:min(1)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteSpecialityAsync(int specialtyId)
        {
            await _specialitiesService.DeleteSpecialityAsync(specialtyId);

            return NoContent();
        }
    }
}