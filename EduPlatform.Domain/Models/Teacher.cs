namespace EduPlatform.Domain.Models
{
    public class Teacher
    {
        public Guid TeacherId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public ICollection<Group> Groups { get; set; } = [];
    }
}
