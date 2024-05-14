using Microsoft.Extensions.Logging;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repository;

namespace Infrastructure.Services
{
    public class CourseService
    {
        private readonly DataContext _context;
        private readonly CourseIncludesService _includesService;
        private readonly CategoryService _categoryService;
        private readonly ProgramDetailsService _programDetailsService;
        private readonly CourseContentService _courseContentService;
        private readonly CourseRepository _courseRepository;
        private readonly ILogger<CourseService> _logger;

        public CourseService(DataContext context,
                             CourseIncludesService includesService,
                             CategoryService categoryService,
                             ProgramDetailsService programDetailsService,
                             ILogger<CourseService> logger,
                             CourseContentService courseContentService,
                             CourseRepository courseRepository)
        {
            _context = context;
            _includesService = includesService;
            _categoryService = categoryService;
            _programDetailsService = programDetailsService;
            _logger = logger;
            _courseContentService = courseContentService;
            _courseRepository = courseRepository;
        }

        public async Task<CourseEntity> CreateCourse(CourseModel model)
        {
            try
            {                
                var category = await _categoryService.CreateAsync(model.Category);

                var courseContent = await _courseContentService.CreateAsync(model.CourseContent);

                var newCourse = new CourseEntity
                {
                    Title = model.Title,
                    Author = model.Author,
                    ImageUrl = model.ImageUrl,
                    AltText = model.AltText,
                    BestSeller = model.BestSeller,
                    Currency = model.Currency,
                    Price = model.Price,
                    DiscountPrice = model.DiscountPrice,
                    LengthInHours = model.LengthInHours,
                    CourseDescription = model.CourseDescription,
                    CategoryId = category.Id,
                    CourseContentId = courseContent.Id
                };
                
                var courseExists = await _courseRepository.ExistsAsync(x => x.Id == newCourse.Id || x.Title == newCourse.Title);
                if (courseExists)
                {
                    return null!; // Find a better way to deal with this - should return conflict 409.
                }
                else
                {
                    var courseIncludesEntities = model.CourseIncludes.Select(include => new CourseIncludesEntity
                    {
                        CourseId = newCourse.Id,
                        FACode = include.FACode,
                        Description = include.Description,
                    }).ToList();

                    var programDetailsEntities = model.ProgramDetails.Select(include => new ProgramDetailsEntity
                    {
                        CourseId = newCourse.Id,
                        Title = include.Title,
                        Description = include.Description,

                    }).ToList();



                    var newCourseEntity = await _courseRepository.CreateAsync(newCourse);
                    if (newCourseEntity != null)
                    {
                        //foreach (var courseIncludesEntity in courseIncludesEntities)
                        //{
                        //    var courseIncludes = await _includesService.CreateAsync(courseIncludesEntity);
                        //}

                        //var result = await _programDetailsService.CreateAsync(newCourseEntity.Id, programDetailsEntities);

                        return newCourseEntity;
                    }
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating the course");
                //throw new ApplicationException("Failed to create the course", ex);
            }
            return null!;
        }
    }
}
