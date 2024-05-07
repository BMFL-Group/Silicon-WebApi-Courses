using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class CourseContentRepository
    {
        private readonly DataContext _context;


        public CourseContentRepository(DataContext context)
        {
            _context = context;
        }

        // Add new CourseContent
        public async Task<CourseContentEntity> AddCourseContentAsync(CourseContentEntity courseContent)
        {
            _context.CourseContents.Add(courseContent);
            await _context.SaveChangesAsync();
            return courseContent;
        }

        // Get a single CourseContent by id
        public async Task<CourseContentEntity> GetCourseContentByIdAsync(int id)
        {
            return await _context.CourseContents.FindAsync(id);
        }

        // Get all CourseContents
        public async Task<List<CourseContentEntity>> GetAllCourseContentsAsync()
        {
            return await _context.CourseContents.ToListAsync();
        }

        // Update a CourseContent
        public async Task<CourseContentEntity> UpdateCourseContentAsync(CourseContentEntity courseContent)
        {
            var entity = await _context.CourseContents.FindAsync(courseContent.Id);
            if (entity == null)
            {
                throw new Exception("Course not found");
            }

            _context.Entry(entity).CurrentValues.SetValues(courseContent);
            await _context.SaveChangesAsync();
            return entity;
        }

        // Delete a CourseContent
        public async Task DeleteCourseContentAsync(int id)
        {
            var courseContent = await _context.CourseContents.FindAsync(id);
            if (courseContent == null)
            {
                throw new Exception("Course not found");
            }

            _context.CourseContents.Remove(courseContent);
            await _context.SaveChangesAsync();
        }
    }
}