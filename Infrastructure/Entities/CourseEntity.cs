using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class CourseEntity
    {
        [Key]
        [Required]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Author { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public string AltText { get; set; } = null!;

        public bool BestSeller { get; set; } = false;

        [Required]
        public string Currency { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        [Required]
        public string LengthInHours { get; set; } = null!;

        public int Rating { get; set; } = 0;

        public int NumberOfReviews { get; set; } = 0;

        public int NumberOfLikes { get; set; } = 0;

        public int? CategoryId { get; set; }

        public virtual CategoryEntity Category { get; set; } = new();

        public string CourseDescription { get; set; } = null!;

        public int? CourseContentId { get; set; }

        public CourseContentEntity CourseContent { get; set; } = new CourseContentEntity();

        public virtual ICollection<ProgramDetailsEntity> ProgramDetails { get; set; } = new List<ProgramDetailsEntity>();

        public virtual ICollection<CourseIncludesEntity> CourseIncludes { get; set; } = new List<CourseIncludesEntity>();
    }
}
