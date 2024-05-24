
namespace Infrastructure.Models
{
    public class SavedCoursesModel
    {
        public int Id { get; set; }
        public string CourseId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public bool IsBookmarked { get; set; } = false;
        public bool HasJoined { get; set; } = false;
    }
    
}
