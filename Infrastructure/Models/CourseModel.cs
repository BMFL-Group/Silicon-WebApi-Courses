
namespace Infrastructure.Models
{
    public class CourseModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Description { get; set; }
        public CategoryModel Category { get; set; }
        public List<ProgramDetailsModel> ProgramDetails { get; set; }
        public List<CourseIncludesModel> Includes { get; set; }
    }
}
