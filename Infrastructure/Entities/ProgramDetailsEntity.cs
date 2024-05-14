using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class ProgramDetailsEntity
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [ForeignKey("Course")]
        public string CourseId { get; set; } 
        public virtual CourseEntity Course { get; set; } = new();

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}