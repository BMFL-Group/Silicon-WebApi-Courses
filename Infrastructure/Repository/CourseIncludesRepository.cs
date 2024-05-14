using System.Linq.Expressions;
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

        public async Task<CourseIncludesEntity> GetOneAsync(Expression<Func<CourseIncludesEntity, bool>> predicate)
        {
            return await _context.CourseIncludes
                .Include(ci => ci.Course)
                .FirstOrDefaultAsync(predicate);
        }
        
        public async Task<bool> DeleteAsync(string courseId)
        {
            if (string.IsNullOrEmpty(courseId))
            {
                return false; 
            }

            try
            {
                var courseInclude = await _context.CourseIncludes
                    .FirstOrDefaultAsync(ci => ci.CourseId == courseId);

                if (courseInclude == null)
                {
                    return false; 
                }

                _context.CourseIncludes.Remove(courseInclude);
                int result = await _context.SaveChangesAsync();
                return result > 0; 
            }
            catch (Exception)
            { 
                return false;
            }
        }

    }
}