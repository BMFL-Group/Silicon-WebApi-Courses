using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Models;

namespace Infrastructure.Services
{
    public class CategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<CategoryEntity> CreateAsync(CategoryModel categoryModel)
        {
            var categoryEntity = new CategoryEntity
            {
                CategoryName = categoryModel.Name
            };
            _context.Categories.Add(categoryEntity);
            await _context.SaveChangesAsync();
            return categoryEntity;
        }
    }
}
