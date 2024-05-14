


namespace Infrastructure.Models
{
    public class CourseModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string? AltText { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public string LengthInHours { get; set; }
        public CategoryModel Category { get; set; }
        public List<ProgramDetailsModel> ProgramDetails { get; set; }
        public List<CourseIncludesModel> Includes { get; set; }
    }
}
