using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class SavedCoursesEntity​
     {
        [Key]
        public int Id { get; set; }
        public string CourseId { get; set; } = null!;
        public string UserId { get; set; } = null!;
    }
}
