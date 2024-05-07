using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Entities;
using Infrastructure.Contexts;

namespace Infrastructure.Repository
{
    public class CourseIncludesRepository
    {
        private readonly DataContext _context;

        public CourseIncludesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CourseIncludesEntity>> GetCourseIncludesByIdAsync(string courseId)
        {
            return await _context.Set<CourseIncludesEntity>()
                                 .Where(ci => ci.CourseId == courseId)
                                 .Include(ci => ci.Course)
                                 .ToListAsync();
        }
    }
}