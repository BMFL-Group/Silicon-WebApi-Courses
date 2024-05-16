using Infrastructure.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
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

        #region Create
        [HttpPost]
        public async Task<IActionResult> CreateProgramDetail( ProgramDetailsDto programDetailsDto)
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
        #endregion

        #region GET ONE

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgramDetail(string id)
        {
            var programDetail = await _programDetailsRepository.GetProgramDetailAsync(id);
            if (programDetail == null)
            {
                return NotFound();
            }
            return Ok(programDetail);
        }
        #endregion

        #region GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAllProgramDetails()
        {
            var details = await _programDetailsRepository.GetAllProgramDetailsAsync();
            return Ok(details);
        }
        #endregion

        #region UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgramDetail(int id, [FromBody] ProgramDetailsEntity programDetail)
        {
            if (id != programDetail.Id)
                return BadRequest("Mismatched ID in request");

            bool updated = await _programDetailsRepository.UpdateProgramDetailAsync(programDetail);
            if (!updated)
                return NotFound();

            return NoContent();
        }
        #endregion

        #region DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramDetail(string id)
        {
            bool deleted = await _programDetailsRepository.DeleteProgramDetailAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
        #endregion
    }
}
