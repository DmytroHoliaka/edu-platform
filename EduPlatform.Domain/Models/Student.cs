namespace EduPlatform.Domain.Models
{
    public class Student
    {
        public Guid StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public Guid? GroupId { get; set; }
        public Group? Group { get; set; }
    }
}
