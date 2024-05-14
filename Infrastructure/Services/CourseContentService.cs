using Infrastructure.Entities;
using Infrastructure.Models;
using Infrastructure.Repository;

namespace Infrastructure.Services;

public class CourseContentService(CourseContentRepository repository)
{
    private readonly CourseContentRepository _repository = repository;

    public async Task<CourseContentEntity> CreateAsync(CourseContentModel model)
    {
        try
        {
            if (model != null)
            {
                var entity = new CourseContentEntity()
                {
                    Strings = model.Strings,
                };
                var result = await _repository.AddCourseContentAsync(entity);
                if(result != null)
                {
                    return result;
                }
            } 
        }
        catch (Exception ex)
        {

        }
        return null!;
    }
}
