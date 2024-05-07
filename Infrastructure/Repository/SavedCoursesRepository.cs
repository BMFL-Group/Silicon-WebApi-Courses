using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SavedCoursesRepository
    {
        private readonly DataContext _context;

        public SavedCoursesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<SavedCoursesModel>> GetSavedCoursesForUserAsync(string userId)
        {
            var savedCourses = await _context.SavedCourses
                .Where(sc => sc.UserId == userId)
                .ToListAsync();

            var savedCourseModels = savedCourses.Select(sc => new SavedCoursesModel
            {
                Id = sc.Id,
                CourseId = sc.CourseId,
                UserId = sc.UserId
            }).ToList();

            return savedCourseModels;
        }

        public async Task<SavedCoursesModel> CreateSavedCourseAsync(SavedCoursesModel savedCourse)
        {
            var entity = new SavedCoursesEntity
            {
                CourseId = savedCourse.CourseId,
                UserId = savedCourse.UserId
            };

            _context.SavedCourses.Add(entity);
            await _context.SaveChangesAsync();

            savedCourse.Id = entity.Id; 
            return savedCourse;
        }

        public async Task<bool> DeleteSavedCourseAsync(int savedCourseId)
        {
            var savedCourse = await _context.SavedCourses.FindAsync(savedCourseId);

            if (savedCourse == null)
                return false;

            _context.SavedCourses.Remove(savedCourse);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}