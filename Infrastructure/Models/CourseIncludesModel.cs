
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Models
{
    public class CourseIncludesModel
    {
        public string CourseId { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string FACode { get; set; } = null!;
    }
}
