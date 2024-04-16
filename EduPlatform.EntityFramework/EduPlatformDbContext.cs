using EduPlatform.Domain.Models;
using EduPlatform.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework
{
    public class EduPlatformDbContext : DbContext
    {
        public DbSet<StudentDto> Students => Set<StudentDto>();

        public EduPlatformDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
