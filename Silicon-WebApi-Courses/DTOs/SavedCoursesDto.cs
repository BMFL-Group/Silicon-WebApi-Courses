namespace Silicon_WebApi_Courses.DTOs
{
    public class SavedCoursesDto
    {
        public int Id { get; set; }
        public string CourseId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public bool IsBookmarked { get; set; }
        public bool IsJoined { get; set; }
    }
}
