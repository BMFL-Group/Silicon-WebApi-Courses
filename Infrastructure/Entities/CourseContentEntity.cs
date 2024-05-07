using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class CourseContentEntity
    {
        [Key]
        public int Id { get; set; }
        public string[] Strings { get; set; } = null!;
    }
}