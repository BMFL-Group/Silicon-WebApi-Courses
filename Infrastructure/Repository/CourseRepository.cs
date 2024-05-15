using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repository
{

    public class CourseRepository(DataContext context) : BaseRepo<CourseEntity, DataContext>(context)
    {
        public override async Task<CourseEntity> GetOneAsync(Expression<Func<CourseEntity, bool>> predicate)
        {
            return await _context.Courses
                .FirstOrDefaultAsync(course => course.Id == id);
        }

        public async Task<IEnumerable<CourseEntity>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }

        // Override to include related entities
        public async Task<CourseEntity> GetCourseWithDetailsAsync(string courseId)
        {
            var course = await _context.Courses
             .Where(predicate)
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

        // OPTIONAL 

        public async Task<bool> CourseExistsAsync(string courseId)
        {
            return await _context.Courses.AnyAsync(c => c.Id == courseId);
        }

        public async Task<CourseEntity> AddCourseIncludeAsync(CourseIncludesEntity courseIncludes)
        {
            var course = await _context.Courses
                .Include(c => c.CourseIncludes)
                .FirstOrDefaultAsync(c => c.Id == courseIncludes.CourseId);

            if (course == null)
            {
                return null;
            }

            course.CourseIncludes.Add(courseIncludes);
            await _context.SaveChangesAsync();
            return course;
        }



    }
}
