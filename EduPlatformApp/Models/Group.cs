namespace EduPlatform.WPF.Models
{
    public class Group
    {
        public Guid GroupId { get; set; }
        public string? Name { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public ICollection<Teacher>? Teachers { get; set; }
        public ICollection<Student>? Students { get; set; }
    }
}
