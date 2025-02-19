﻿namespace EduPlatform.Domain.Models
{
    public class Course
    {
        public Guid CourseId { get; init; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Group> Groups { get; set; } = [];
    }
}
