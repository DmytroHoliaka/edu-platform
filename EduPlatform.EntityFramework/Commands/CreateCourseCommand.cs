﻿using EduPlatform.Domain.Commands;
using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DbContextConfigurations;
using EduPlatform.EntityFramework.Service;

namespace EduPlatform.EntityFramework.Commands
{
    public class CreateCourseCommand(EduPlatformDbContextFactory contextFactory) : ICreateCourseCommand
    {
        public async Task ExecuteAsync(Course? newCourse)
        {
            ArgumentNullException.ThrowIfNull(newCourse, nameof(newCourse));

            using (EduPlatformDbContext context = contextFactory.Create())
            {
                Course? duplicateCourse = context.Courses.FirstOrDefault(c => c.Name == newCourse.Name);

                if (duplicateCourse is not null)
                {
                    throw new InvalidDataException("The course with this name already exists");
                }

                EntityMapper.SetCourseDbRelationships(context, newCourse);

                await context.Courses.AddAsync(newCourse);
                await context.SaveChangesAsync();
            }
        }
    }
}