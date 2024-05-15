﻿using Infrastructure.Contexts;
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

        public async Task<CourseEntity> AddAsync(CourseEntity course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }
        public async Task<bool> CourseExistsAsync(string courseId)
        {
            return await _context.Courses.AnyAsync(c => c.Id == courseId);
        }

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

        public async Task<IEnumerable<CourseEntity>> GetAllCoursesAsync()
        {
            return await _context.Courses.ToListAsync();
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