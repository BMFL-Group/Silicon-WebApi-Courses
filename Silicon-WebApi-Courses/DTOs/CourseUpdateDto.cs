using Infrastructure.Models;
using System.ComponentModel.DataAnnotations;

public class CourseUpdateDto
{

    public string Title { get; set; } = null!;
    public string Author { get; set; } = null!;   
    public string ImageUrl { get; set; } = null!;
    public string AltText { get; set; } = null!;
    public bool BestSeller { get; set; } = false;
    public string Currency { get; set; } = null!; 
    public decimal Price { get; set; } 
    public decimal? DiscountPrice { get; set; }
    public string LengthInHours { get; set; } = null!;
    public int Rating { get; set; } = 0;
    public int NumberOfReviews { get; set; } = 0;
    public int NumberOfLikes { get; set; } = 0;
    public string CourseDescription { get; set; } = null!;
    public int? CategoryId { get; set; }
    public CategoryModel Category { get; set; } = new();
    public int? CourseContentId { get; set; }
    public CourseContentModel CourseContent { get; set; } = new();

}