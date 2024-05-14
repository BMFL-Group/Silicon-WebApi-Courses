
namespace Infrastructure.Models
{
    public class SavedCoursesModel
    {
        public int Id { get; set; }
        public string CourseId { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
    
}
