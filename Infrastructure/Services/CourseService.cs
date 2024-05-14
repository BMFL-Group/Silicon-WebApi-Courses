using Microsoft.Extensions.Logging;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Services
{
    public class CourseService
    {
        private readonly DataContext _context;
        private readonly CourseIncludesService _includesService;
        private readonly CategoryService _categoryService;
        private readonly ProgramDetailsService _programDetailsService;
        private readonly ILogger<CourseService> _logger;

        public CourseService(DataContext context,
                             CourseIncludesService includesService,
                             CategoryService categoryService,
                             ProgramDetailsService programDetailsService,
                             ILogger<CourseService> logger)
        {
            _context = context;
            _includesService = includesService;
            _categoryService = categoryService;
            _programDetailsService = programDetailsService;
            _logger = logger;
        }

        public async Task<CourseEntity> CreateCourse(CourseModel model)
        {
            try
            {
                var category = await _categoryService.CreateAsync(model.Category);

                var newCourse = new CourseEntity
                {
                    Title = model.Title,
                    Author = model.Author,
                    ImageUrl = model.ImageUrl,
                    AltText = model.AltText,
                    Currency = model.Currency,
                    Price = model.Price,
                    DiscountPrice = model.DiscountPrice,
                    LengthInHours = model.LengthInHours,
                    CourseDescription = model.Description,
                    CategoryId = category.Id,
                };

                var courseIncludesEntities = model.Includes.Select(include => new CourseIncludesEntity
                {
                    Description = include.Description,
                    FACode = include.FACode,
                    CourseId = newCourse.Id 
                }).ToList();

                await _context.Courses.AddAsync(newCourse);
                await _context.SaveChangesAsync();

                if (newCourse != null)
                {
                    foreach (var courseIncludesEntity in courseIncludesEntities)
                    {
                        var courseIncludes = await _includesService.CreateAsync(courseIncludesEntity);
                    }

                }

                return newCourse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating the course");
                throw new ApplicationException("Failed to create the course", ex);
            }
        }
    }
}
