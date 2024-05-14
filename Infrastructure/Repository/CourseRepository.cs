using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.Repository
{

    public class CourseRepository(DataContext context) : BaseRepo<CourseEntity, DataContext>(context)
    {
        public override async Task<CourseEntity> GetOneAsync(Expression<Func<CourseEntity, bool>> predicate)
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

        //public async Task<bool> CourseExists(string courseId)
        //{
        //    return await _context.Courses.AnyAsync(c => c.Id == courseId);
       // }
    }
}
