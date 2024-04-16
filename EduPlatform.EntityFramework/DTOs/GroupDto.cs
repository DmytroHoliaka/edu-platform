namespace EduPlatform.EntityFramework.DTOs
{
    public class GroupDto
    {
        public Guid GroupId { get; set; }
        public string? Name { get; set; }

        public Guid? CourseId { get; set; }
        public CourseDto? Course { get; set; }

        public ICollection<TeacherDto> Teachers { get; set; } = [];
        public ICollection<StudentDto> Students { get; set; } = [];
    }
}
