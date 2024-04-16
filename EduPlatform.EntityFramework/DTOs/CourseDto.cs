namespace EduPlatform.EntityFramework.DTOs
{
    public class CourseDto
    {
        public Guid CourseId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<GroupDto> Groups { get; set; } = [];
    }
}
