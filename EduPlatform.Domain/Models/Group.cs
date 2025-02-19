﻿namespace EduPlatform.Domain.Models
{
    public class Group
    {
        public Guid GroupId { get; init; }
        public string? Name { get; set; }

        public Guid? CourseId { get; set; }
        public Course? Course { get; set; }

        public ICollection<Teacher> Teachers { get; set; } = [];
        public ICollection<Student> Students { get; set; } = [];
    }
}
