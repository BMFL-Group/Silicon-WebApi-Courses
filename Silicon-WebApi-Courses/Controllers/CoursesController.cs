using Infrastructure.Models;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Silicon_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly CourseRepository _courseRepository;
        private readonly CourseService _courseService;

        public CoursesController(CourseRepository courseRepository, CourseService courseService)
        {
            _courseRepository = courseRepository;
            _courseService = courseService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createdCourse = await _courseService.CreateCourse(model);
                return CreatedAtAction(nameof(GetById), new { id = createdCourse.Id }, createdCourse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var course = await _courseRepository.GetOneAsync(c => c.Id == id);
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
                var result = await _courseRepository.GetAllAsync();
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CourseUpdateDto updatedCourseDto)
        {
            if (updatedCourseDto == null)
            {
                return BadRequest("Update data cannot be null.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var course = await _courseRepository.GetOneAsync(c => c.Id == id);
                if (course == null)
                {
                    return NotFound($"Course with ID {id} not found.");
                }
                else
                {
                    course.Title = updatedCourseDto.Title ?? course.Title;
                    course.Author = updatedCourseDto.Author ?? course.Author;
                    course.ImageUrl = updatedCourseDto.ImageUrl ?? course.ImageUrl; 
                    course.AltText = updatedCourseDto.AltText ?? course.AltText;
                    course.BestSeller = updatedCourseDto.BestSeller = updatedCourseDto.BestSeller;
                    course.Currency = updatedCourseDto.Currency ?? course.Currency;
                    course.Price = updatedCourseDto.Price <= 1 ? updatedCourseDto.Price : course.Price;
                    course.DiscountPrice = updatedCourseDto.DiscountPrice >= 1 ? updatedCourseDto.DiscountPrice : course.DiscountPrice;
                    course.LengthInHours = updatedCourseDto.LengthInHours ?? course.LengthInHours;
                    course.Rating = updatedCourseDto.Rating <= 0 ? updatedCourseDto.Rating : course.Rating;
                    course.NumberOfReviews = updatedCourseDto.NumberOfReviews >= 0 ? updatedCourseDto.NumberOfReviews : course.NumberOfReviews ;
                    course.NumberOfLikes = updatedCourseDto.NumberOfLikes >= 0 ? updatedCourseDto.NumberOfLikes : course.NumberOfLikes;
                    course.CategoryId = updatedCourseDto.CategoryId <= 1 ? updatedCourseDto.CategoryId : course.CategoryId;
                    course.CourseDescription = updatedCourseDto.CourseDescription ?? course.CourseDescription;
                    course.CourseContentId = updatedCourseDto.CourseContentId ?? course.CourseContentId;
                }           

                
                bool updateSuccess = await _courseRepository.UpdateAsync(course);
                if (updateSuccess)
                {
                   
                    var updatedCourseEntity = await _courseRepository.GetOneAsync(c => c.Id == id);
                    return Ok(updatedCourseEntity);
                }
                else
                {
                    return StatusCode(500, "An error occurred while updating the course.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var course = await _courseRepository.GetOneAsync(c => c.Id == id);
                if (course == null)
                {
                    return NotFound($"Course with ID {id} not found.");
                }

                bool deleteSuccess = await _courseRepository.DeleteAsync(course);
                if (deleteSuccess)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(500, "Something went wrong when deleting the course.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}