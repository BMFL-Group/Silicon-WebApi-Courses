using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Silicon_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseRepository _courseRepository;

        public CoursesController(CourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseEntity course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (course == null)
            {
                return BadRequest("Course data is null.");
            }

            try
            {
                await _courseRepository.AddAsync(course);
                return CreatedAtAction(nameof(GetById), new { id = course.Id }, course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course != null)
            {
                return Ok(course);
            }
            else
            {
                return NotFound($"Course with ID {id} not found.");
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _courseRepository.GetAllCoursesAsync();
                if (result != null)
                {
                    return Ok(result);
                }
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}