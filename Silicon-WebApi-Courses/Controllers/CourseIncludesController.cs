using Infrastructure.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Silicon_WebApi_Courses.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Silicon_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseIncludesController : ControllerBase
    {
        private readonly CourseIncludesRepository _courseIncludesRepository;
        private readonly CourseRepository _courseRepository;
        private readonly ILogger<CourseIncludesController> _logger;

        public CourseIncludesController(
            CourseIncludesRepository courseIncludesRepository,
            CourseRepository courseRepository,
            ILogger<CourseIncludesController> logger)
        {
            _courseIncludesRepository = courseIncludesRepository;
            _courseRepository = courseRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<CourseIncludesDTO>> CreateCourseIncludes(CourseIncludesDTO courseIncludesDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for CourseIncludesDTO.");
                return BadRequest(ModelState);
            }

            try
            {
                // Validate that the CourseId exists using CourseRepository
                var courseExists = await _courseRepository.CourseExistsAsync(courseIncludesDTO.CourseId);
                if (!courseExists)
                {
                    return BadRequest("Invalid CourseId. The specified course does not exist.");
                }

                var courseIncludes = new CourseIncludesEntity
                {
                    CourseId = courseIncludesDTO.CourseId,
                    Description = courseIncludesDTO.Description,
                    FACode = courseIncludesDTO.FACode,
                };

                var createdCourseIncludes = await _courseIncludesRepository.AddCourseIncludesAsync(courseIncludes);

                var createdCourseIncludesDTO = new CourseIncludesDTO
                {
                    CourseId = createdCourseIncludes.CourseId,
                    Description = createdCourseIncludes.Description,
                    FACode = createdCourseIncludes.FACode
                };

                return CreatedAtAction(nameof(GetCourseIncludes), new { courseId = createdCourseIncludes.CourseId }, createdCourseIncludesDTO);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, "A database error occurred while creating the course includes.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong when creating the course includes.");
            }
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<List<CourseIncludesDTO>>> GetCourseIncludes(string courseId)
        {
            var courseIncludes = await _courseIncludesRepository.GetCourseIncludesByIdAsync(courseId);
            if (courseIncludes == null || courseIncludes.Count == 0)
                return NotFound("No includes found for the specified course.");

            var courseIncludesDTOs = courseIncludes.Select(ci => new CourseIncludesDTO
            {
                CourseId = ci.CourseId,
                Description = ci.Description,
                FACode = ci.FACode
            }).ToList();

            return Ok(courseIncludesDTOs);
        }

        [HttpDelete("{courseId}")]
        public async Task<IActionResult> Delete(string courseId)
        {
            bool deleteSuccess = await _courseIncludesRepository.DeleteAsync(courseId);
            if (deleteSuccess)
            {
                return NoContent();
            }
            else
            {
                return NotFound("No matching course includes found or deletion failed.");
            }
        }

        [HttpPut("{courseId}")]
        public async Task<IActionResult> UpdateCourseIncludes(string courseId, [FromBody] CourseIncludesDTO courseIncludesDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (courseId != courseIncludesDTO.CourseId)
                {
                    return BadRequest("CourseId in path and request body need to match!");
                }

                var courseExists = await _courseRepository.CourseExistsAsync(courseIncludesDTO.CourseId);
                if (!courseExists)
                {
                    
                    return BadRequest("Invalid CourseId. The specified course does not exist.");
                }

                var existingIncludes = await _courseIncludesRepository.GetCourseIncludesByIdAsync(courseId);
                if (existingIncludes == null || !existingIncludes.Any())
                {
                    return NotFound("No includes found for the specified course.");
                }

               
                var includeToUpdate = existingIncludes.First(); 
                includeToUpdate.Description = courseIncludesDTO.Description;
                includeToUpdate.FACode = courseIncludesDTO.FACode;

                var updatedInclude = await _courseIncludesRepository.UpdateCourseIncludesAsync(includeToUpdate);
                var updatedCourseIncludesDTO = new CourseIncludesDTO
                {
                    CourseId = updatedInclude.CourseId,
                    Description = updatedInclude.Description,
                    FACode = updatedInclude.FACode
                };

                return Ok(updatedCourseIncludesDTO);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, "A database error occurred while updating the course includes.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong when updating the course includes.");
            }
        }
    }
}
