using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Services
{
    public class CourseIncludesService
    {
        private readonly DataContext _context;

        public CourseIncludesService(DataContext context)
        {
            _context = context;
        }
        
        public async Task<CourseIncludesEntity> CreateAsync (CourseIncludesEntity courseIncludes)
        {
            _context.CourseIncludes.Add(courseIncludes);
            await _context.SaveChangesAsync();
            return courseIncludes;
        }
    }
}