using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CourseIncludesRepository
    {
        private readonly DataContext _context;
        private readonly ILogger<CourseIncludesRepository> _logger;

        public CourseIncludesRepository(DataContext context, ILogger<CourseIncludesRepository> logger)
        {
            _context = context;
            _logger = logger;
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
            catch (DbUpdateException ex)
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

        public async Task<CourseIncludesEntity> UpdateCourseIncludesAsync(CourseIncludesEntity courseIncludes)
        {
            try
            {
                _context.CourseIncludes.Update(courseIncludes);
                await _context.SaveChangesAsync();
                return courseIncludes;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
