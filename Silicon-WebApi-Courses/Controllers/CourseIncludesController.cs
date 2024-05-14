﻿using Infrastructure.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Silicon_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseIncludesController : ControllerBase
    {
        private readonly CourseIncludesRepository _courseIncludesRepository;

        public CourseIncludesController(CourseIncludesRepository courseIncludesRepository)
        {
            _courseIncludesRepository = courseIncludesRepository;
        }

        [HttpGet("{courseId}")]
        public async Task<ActionResult<List<CourseIncludesEntity>>> GetCourseIncludes(int courseId)
        {
            var courseIncludes = await _courseIncludesRepository.GetCourseIncludesByIdAsync(courseId.ToString());
            if (courseIncludes == null || courseIncludes.Count == 0)
                return NotFound("No includes found for the specified course.");

            return Ok(courseIncludes);
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
        
    }
}