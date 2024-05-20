using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Silicon_WebApi_Courses.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Silicon_WebApi_Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedCoursesController : ControllerBase
    {
        private readonly SavedCoursesRepository _savedCoursesRepository;

        public SavedCoursesController(SavedCoursesRepository savedCoursesRepository)
        {
            _savedCoursesRepository = savedCoursesRepository;
        }

        #region GET
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<SavedCoursesDto>>> GetSavedCoursesForUser(string userId)
        {
            var savedCourses = await _savedCoursesRepository.GetSavedCoursesForUserAsync(userId);
            if (savedCourses == null || savedCourses.Count == 0)
                return NotFound("No saved courses found for the specified user.");

            var savedCoursesDto = savedCourses.Select(sc => new SavedCoursesDto
            {
                Id = sc.Id,
                CourseId = sc.CourseId,
                UserId = sc.UserId,
                IsBookmarked = sc.IsBookmarked,
                IsJoined = sc.IsJoined
            }).ToList();

            return Ok(savedCoursesDto);
        }
        #endregion

        #region CREATE
        [HttpPost]
        public async Task<ActionResult<SavedCoursesDto>> CreateSavedCourse([FromBody] SavedCoursesDto savedCourseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var savedCourseModel = new SavedCoursesModel
            {
                CourseId = savedCourseDto.CourseId,
                UserId = savedCourseDto.UserId,
                IsBookmarked = savedCourseDto.IsBookmarked,
                IsJoined = savedCourseDto.IsJoined
            };

            var createdCourse = await _savedCoursesRepository.CreateSavedCourseAsync(savedCourseModel);
            savedCourseDto.Id = createdCourse.Id;

            return CreatedAtAction(nameof(GetSavedCoursesForUser), new { userId = createdCourse.UserId }, savedCourseDto);
        }
        #endregion

        #region UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSavedCourse(int id, [FromBody] SavedCoursesDto savedCourseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var savedCourseModel = new SavedCoursesModel
            {
                Id = id,
                CourseId = savedCourseDto.CourseId,
                UserId = savedCourseDto.UserId,
                IsBookmarked = savedCourseDto.IsBookmarked,
                IsJoined = savedCourseDto.IsJoined
            };

            var updated = await _savedCoursesRepository.UpdateSavedCourseAsync(savedCourseModel);
            if (!updated)
                return NotFound($"Saved course with ID {id} not found.");

            return NoContent();
        }
        #endregion

        #region DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSavedCourse(int id)
        {
            var deleted = await _savedCoursesRepository.DeleteSavedCourseAsync(id);
            if (!deleted)
                return NotFound($"Saved course with ID {id} not found.");

            return NoContent();
        }
        #endregion
    }
}