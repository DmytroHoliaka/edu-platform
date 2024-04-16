using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework
{
    public class EduPlatformDbContextFactory
    {
        private readonly DbContextOptions _options;

        public EduPlatformDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public EduPlatformDbContext Create()
        {
            return new EduPlatformDbContext(_options);
        }
    }
}
