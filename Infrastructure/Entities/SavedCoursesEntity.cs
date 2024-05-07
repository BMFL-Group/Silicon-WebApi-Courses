using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class SavedCoursesEntity​
     {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string UserId { get; set; }
    }
}
