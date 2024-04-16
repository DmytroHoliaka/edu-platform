namespace EduPlatform.EntityFramework.DTOs
{
    public class TeacherDto
    {
        public Guid TeacherId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<GroupDto> Groups { get; set; } = [];
    }
}
