using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Repository
{
    public class CategoryRepository : BaseRepo<CategoryEntity, DataContext>
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }
        public override async Task<List<CategoryEntity>> GetAllAsync()
        {
            try
            {
                // Order categories alphabetically and return as a List
                return await _context.Categories.OrderBy(c => c.CategoryName).ToListAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR :: {ex.Message}");
                // Error handling?
                return new List<CategoryEntity>();
            }
        }
    }
}
