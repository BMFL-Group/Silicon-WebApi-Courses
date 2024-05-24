using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repositories
{
    public class SavedCoursesRepository
    {
        private readonly DataContext _context;

        public SavedCoursesRepository(DataContext context)
        {
            _context = context;
        }

        #region GET
        public async Task<IEnumerable<SavedCoursesModel>> GetSavedCoursesForUserAsync(string userId)
        {
            return await _context.SavedCourses
                .Where(sc => sc.UserId == userId)
                .Select(sc => new SavedCoursesModel
                {
                    Id = sc.Id,
                    CourseId = sc.CourseId,
                    UserId = sc.UserId,
                    IsBookmarked = sc.IsBookmarked,
                    HasJoined = sc.HasJoined
                })
                .ToListAsync();
        }
        #endregion

        #region CREATE
        public async Task<SavedCoursesModel> CreateSavedCourseAsync(SavedCoursesModel savedCourse)
        {
            var entity = new SavedCoursesEntity
            {
                CourseId = savedCourse.CourseId,
                UserId = savedCourse.UserId,
                IsBookmarked = savedCourse.IsBookmarked,
                HasJoined = savedCourse.HasJoined
            };

            _context.SavedCourses.Add(entity);
            await _context.SaveChangesAsync();

            savedCourse.Id = entity.Id;
            return savedCourse;
        }
        #endregion

        #region UPDATE
        public async Task<bool> UpdateSavedCourseAsync(SavedCoursesModel savedCourse)
        {
            try
            {
                var entity = await _context.SavedCourses.FirstOrDefaultAsync(x => x.Id == savedCourse.Id);
                if (entity == null) return false;

                var updatedEntity = new SavedCoursesEntity()
                {
                    Id = savedCourse.Id,
                    CourseId = savedCourse.CourseId,
                    UserId = savedCourse.UserId,
                    IsBookmarked = savedCourse.IsBookmarked,
                    HasJoined = savedCourse.HasJoined
                };

                _context.SavedCourses.Entry(entity).CurrentValues.SetValues(updatedEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }
        #endregion

        #region DELETE
        public async Task<bool> DeleteSavedCourseAsync(int savedCourseId)
        {
            var savedCourse = await _context.SavedCourses.FindAsync(savedCourseId);
            if (savedCourse == null) return false;

            _context.SavedCourses.Remove(savedCourse);
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion
    }
}
