using EduPlatform.Domain.Models;
using EduPlatform.Domain.Queries;
using EduPlatform.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.Queries
{
    public class GetAllStudentsQuery : IGetAllStudentsQuery
    {
        public readonly EduPlatformDbContextFactory _contextFactory;

        public GetAllStudentsQuery(EduPlatformDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Student>> ExecuteAsync()
        {
            using (EduPlatformDbContext context = _contextFactory.Create())
            {
                IEnumerable<StudentDto> studentDtos = await context.Students.ToListAsync();

                return studentDtos.Select(dto => new Student()
                {
                    StudentId = dto.StudentId,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    GroupId = dto.GroupId,
                    Group = dto.Group,
                });
            }
        }
    }
}
