using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class CourseIncludesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Add this attribute
        public string Id { get; set; } // Remove the default assignment with Guid.NewGuid()

        [ForeignKey("Course")]
        public string CourseId { get; set; }
        public virtual CourseEntity Course { get; set; } = new();
        public string FACode { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}