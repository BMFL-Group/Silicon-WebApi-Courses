using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace Infrastructure.Repository
{
    public class CourseRepository : BaseRepo<CourseEntity, DataContext>
    {
        public CourseRepository(DataContext context) : base(context)
        {
        }

        #region ADD
        public async Task<CourseEntity> AddAsync(CourseEntity course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }
        #endregion

        #region Exists
        public async Task<bool> CourseExistsAsync(string courseId)
        {
            return await _context.Courses.AnyAsync(c => c.Id == courseId);
        }
        #endregion

        #region GET BY ID

        public async Task<CourseEntity> GetByIdAsync(string id)
        {
            try
            {
                var result = await _context.Courses.FirstOrDefaultAsync(course => course.Id == id);
                if (result != null)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!;
        }
        #endregion

        #region GET ALL
        public async Task<IEnumerable<CourseEntity>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
        }
        #endregion

        #region GET COURSE WITH DETAILS
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
        #endregion
    }
}