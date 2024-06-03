using Microsoft.EntityFrameworkCore;

namespace EduPlatform.EntityFramework.DbContextConfigurations
{
    public class EduPlatformDbContextFactory(DbContextOptions options)
    {
        public EduPlatformDbContext Create()
        {
            return new EduPlatformDbContext(options);
        }
    }
}
