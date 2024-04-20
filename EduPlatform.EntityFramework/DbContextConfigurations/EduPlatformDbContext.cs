using EduPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.DbContextConfigurations
{
    public class EduPlatformDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Teachers)
                .WithMany(t => t.Groups)
                .UsingEntity<Dictionary<string, object>>(
                    j => j.HasOne<Teacher>()
                          .WithMany()
                          .HasForeignKey("TeacherId"),

                    j => j.HasOne<Group>()
                          .WithMany()
                          .HasForeignKey("GroupId"),

                    j => j.ToTable("GroupTeacherMap")
                );
        }
    }
}
