using Infrastructure.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Silicon_WebApi_Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProgramDetailsController : ControllerBase
    {
        private readonly ProgramDetailsRepository _programDetailsRepository;
        private readonly CourseRepository _courseRepository;

        public ProgramDetailsController(ProgramDetailsRepository programDetailsRepository, CourseRepository courseRepository)
        {
            _programDetailsRepository = programDetailsRepository;
            _courseRepository = courseRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProgramDetail([FromBody] ProgramDetailsDto programDetailsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool courseExists = await _courseRepository.CourseExistsAsync(programDetailsDto.CourseId);
            if (!courseExists)
            {
                return BadRequest("Invalid CourseId: The specified course does not exist.");
            }

            var newDetail = new ProgramDetailsEntity
            {
                CourseId = programDetailsDto.CourseId,
                Title = programDetailsDto.Title,
                Description = programDetailsDto.Description
            };

            try
            {
                var createdDetail = await _programDetailsRepository.AddProgramDetailAsync(newDetail);
                return CreatedAtAction(nameof(GetProgramDetail), new { id = createdDetail.Id }, createdDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the program details: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgramDetail(int id) 
        {
            try
            {
                var programDetail = await _programDetailsRepository.GetProgramDetailAsync(id);
                if (programDetail == null)
                {
                    return NotFound();
                }
                return Ok(programDetail);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProgramDetails()
        {
            var details = await _programDetailsRepository.GetAllProgramDetailsAsync();
            return Ok(details);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgramDetail(int id, [FromBody] ProgramDetailsDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var programDetail = await _programDetailsRepository.GetProgramDetailAsync(id);
            if (programDetail == null)
            {
                return NotFound($"No program detail found with ID {id}.");
            }

            programDetail.Title = updateDto.Title ?? programDetail.Title;
            programDetail.Description = updateDto.Description ?? programDetail.Description;

            try
            {
                bool updated = await _programDetailsRepository.UpdateProgramDetailAsync(programDetail);
                if (!updated)
                {
                    return NotFound("Update failed or no changes made.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the program details: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramDetail(int id)
        {
            bool deleted = await _programDetailsRepository.DeleteProgramDetailAsync(id);
            if (!deleted)
            {
                return NotFound();

            }
            return NoContent();
        }
    }
}