using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repository
{
    public class CourseIncludesRepository
    {
        private readonly DataContext _context;


        public CourseIncludesRepository(DataContext context, ILogger<CourseIncludesRepository> logger)
        {
            _context = context;
        }

        public async Task<List<CourseIncludesEntity>> GetCourseIncludesByIdAsync(string courseId)
        {
            return await _context.CourseIncludes
                                 .Where(ci => ci.CourseId == courseId)
                                 .ToListAsync();
        }

        public async Task<CourseIncludesEntity> AddCourseIncludesAsync(CourseIncludesEntity courseIncludes)
        {
            try
            {
                _context.CourseIncludes.Add(courseIncludes);
                await _context.SaveChangesAsync();
                return courseIncludes;
            }
            catch 
            {
                throw; 
            }
        }

        public async Task<bool> DeleteAsync(string courseId)
        {
            var courseIncludes = await _context.CourseIncludes
                                               .Where(ci => ci.CourseId == courseId)
                                               .ToListAsync();

            if (!courseIncludes.Any())
            {
                return false;
            }

            _context.CourseIncludes.RemoveRange(courseIncludes);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
