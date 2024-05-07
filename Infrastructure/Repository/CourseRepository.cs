using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{
    public class CourseRepository : BaseRepo<CourseEntity, DataContext>
    {
        public CourseRepository(DataContext context) : base(context)
        {
        }

        // Override to include related entities
        public async Task<CourseEntity> GetCourseWithDetailsAsync(string courseId)
        {
            var course = await _context.Courses
                .Where(course => course.Id == courseId)
                .Include(course => course.Category)
                .Include(course => course.CourseContent)
                .Include(course => course.CourseIncludes)
                .Include(course => course.ProgramDetails)
                .AsNoTracking() // better performance? 
                .FirstOrDefaultAsync();

            if (course == null)
            {
                // error handling ?
                return null;  
            }
            return course; // return to found course
        }
    }
}

